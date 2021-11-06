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

        private Vector2 movement;

        #endregion


        #region Update

        /// <summary>
        /// Get the Keyboard Inputs
        /// </summary>
        private void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        #endregion


        #region FixedUpdate

        /// <summary>
        /// Move The Player
        /// </summary>
        private void FixedUpdate()
        {

            double verticalHeightSeen = Camera.main.orthographicSize;
            double horizontalHeightSeen = verticalHeightSeen * Screen.width / Screen.height;
            horizontalHeightSeen -= (horizontalHeightSeen / 10) * 3.4;

            Vector3 newPositon = rb.position + movement * playerSpeed * Time.deltaTime;

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

