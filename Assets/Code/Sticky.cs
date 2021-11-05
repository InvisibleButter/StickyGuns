using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Sticky : MonoBehaviour
{
    //Debug only
    public bool isChild;

    public bool isSticky;
    public bool isReciever;

    public float seperationSpeed;
    public float seperationRotationAngle;

    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if(isSticky)rigidbody.velocity = new Vector2(-1, 0);
        else
            rigidbody.velocity = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isChild && transform.parent.position.x > 5)
        {
            seperate();
        }
    }

    public void changeLayer(int layer)
    {
        gameObject.layer = layer;
    }

    public void setParent(Transform transform)
    {
        this.transform.parent = transform;
    }

    public void disableRigidBody()
    {
        rigidbody.isKinematic = true;
    }

    public void seperate()
    {
        Vector2 direction = transform.position - transform.parent.position;
        direction = direction.normalized;
        isSticky = true;
        isReciever = false;
        gameObject.layer = 8;
        rigidbody.isKinematic = false;
        rigidbody.velocity = direction * seperationSpeed;
        rigidbody.angularVelocity = seperationRotationAngle;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        Sticky stic = collision.gameObject.GetComponent<Sticky>();
        if (stic != null&&stic.isSticky&&isReciever)
        {
            Debug.Log("Collision");
            stic.changeLayer(gameObject.layer);
            stic.setParent(transform);
            stic.disableRigidBody();
            stic.isSticky = false;
            stic.isReciever = true;
        }
    }


}
