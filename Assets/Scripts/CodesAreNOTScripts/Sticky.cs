using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Sticky : MonoBehaviour
{
    private WeaponManager weaponManager;

    public bool isSticky;
    public bool isReciever;

    public float seperationSpeed;
    public float seperationRotationAngle;

    Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void changeLayer(int layer)
    {
        gameObject.layer = layer;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.layer = layer;
        }
    }

    public void setParent(Transform transform)
    {
        this.transform.parent = transform;
    }

    public void disableRigidBody()
    {
        rigidbody.isKinematic = true;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void seperate()
    {
        setParent(transform.parent.parent);
        Vector2 direction = transform.position - transform.parent.position;
        direction = direction.normalized;
        isSticky = true;
        isReciever = false;
        gameObject.layer = 8;
        rigidbody.isKinematic = false;
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.AddForce(direction * seperationSpeed);
        rigidbody.AddTorque(seperationRotationAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Sticky stic = collision.gameObject.GetComponent<Sticky>();
        if (stic != null && stic.isSticky && isReciever)
        {
            Weapon otherWeapon = collision.gameObject.GetComponentInChildren<Weapon>();
            Weapon ownWeapon = null;

            if (GetComponent<PlayerEntity>() == null)
            {
                ownWeapon = GetComponentInChildren<Weapon>();
            }

            if(ownWeapon != null)
            {
                if(otherWeapon.Weapontype == ownWeapon.Weapontype)
                {
                    if(ownWeapon.MayLvlUp())
                    {
                        otherWeapon.DestroyMe();
                    }
                    else
                    {
                        Stick(stic, otherWeapon);
                    }
                  
                }
                else
                {
                    Stick(stic, otherWeapon);
                }
            }
            else
            {
                Stick(stic, otherWeapon);
            }
        }
    }

    private void Stick(Sticky stic, Weapon w)
    {
        Debug.Log("Collision");
        stic.changeLayer(gameObject.layer);
        stic.setParent(transform);
        stic.disableRigidBody();
        stic.isSticky = false;
        stic.isReciever = true;
        WeaponManager.Instance.Register(w);
    }
}
