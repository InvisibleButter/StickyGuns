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
        Vector2 direction = transform.position - transform.parent.position;
        direction = direction.normalized;
        isSticky = true;
        isReciever = false;
        gameObject.layer = 8;
        rigidbody.isKinematic = false;
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.velocity = direction * seperationSpeed;
        rigidbody.angularVelocity = seperationRotationAngle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Sticky stic = collision.gameObject.GetComponent<Sticky>();
        if (stic != null && stic.isSticky && isReciever)
        {
            Weapon otherWeapon = collision.gameObject.GetComponentInChildren<Weapon>();
            Weapon ownWeapon = GetComponentInChildren<Weapon>();

            if(ownWeapon != null)
            {
                if(otherWeapon.Weapontype == ownWeapon.Weapontype)
                {
                    ownWeapon.MayLvlUp();
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
