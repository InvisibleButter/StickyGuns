using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StickyGuns.Movement
{
    public class MovementController : MonoBehaviour
    {

        #region Properties

        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float playerSpeed;
        [SerializeField] private float horizontalScaler;

        private Vector2 movement;

        public float boostEnergy = 100;
        private int boostPower = 1;
        private bool boostMode = false;

        #endregion


        public float BoostEnergy
        {
            get
            {
                return boostEnergy;
            }

            set
            {
                boostEnergy = value;

                if (boostEnergy > 100)
                {
                    boostEnergy = 100;
                }

                if (boostEnergy < 0)
                {
                    boostEnergy = 0;
                }
            }
        }


        #region Update

        /// <summary>
        /// Get the Keyboard Inputs
        /// </summary>
        private void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.LeftShift) == true)
            {
                boostMode = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) == true)
            {
                boostMode = false;
            }

        }

        #endregion


        #region FixedUpdate

        /// <summary>
        /// Move The Player
        /// </summary>
        private void FixedUpdate()
        {

            if (BoostEnergy == 0)
            {
                boostMode = false;
            }


            if (boostMode)
            {
                boostPower = 2;
                BoostEnergy -= Time.deltaTime * 100;
            }
            else
            {
                boostPower = 1;
                BoostEnergy += Time.deltaTime * 10;
            }

            double verticalHeightSeen = Camera.main.orthographicSize;
            double horizontalHeightSeen = verticalHeightSeen * Screen.width / Screen.height;
            horizontalHeightSeen -= (horizontalHeightSeen / 10) * horizontalScaler;

            Vector3 newPositon = rb.position + movement * playerSpeed * boostPower * Time.deltaTime;

            if (newPositon.x > horizontalHeightSeen - 0.5)
            {
                newPositon.x = (float)horizontalHeightSeen - 0.5f;
            }

            if (newPositon.x < -horizontalHeightSeen + 0.5)
            {
                newPositon.x = (float)-horizontalHeightSeen + 0.5f;
            }

            if (newPositon.y > verticalHeightSeen - 0.5)
            {
                newPositon.y = (float)verticalHeightSeen - 0.5f;
            }

            if (newPositon.y < -verticalHeightSeen + 0.5)
            {
                newPositon.y = (float)-verticalHeightSeen + 0.5f;
            }

            rb.MovePosition(newPositon);
        }

        #endregion

    }
}

