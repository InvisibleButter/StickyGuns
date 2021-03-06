using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public float Speed = 20f;
    public int Damage = 1;

    private bool _isActive;
    protected Transform _weaponTrans;

    protected int shootFromLayer;

    public enum Type
    {
        Standard,
        Split,
        TargetLock,
        Laser
    }

    public virtual Type BulletType => Type.Standard;

    public bool IsActive 
    { 
        get => _isActive;
        set 
        {
            _isActive = value;
            if(!_isActive)
            {
                DeInitBullet();
            }
        } 
    }

    public virtual void InitBullet(Transform t, int damage, int shootFromLayer)
    {
        _weaponTrans = t;
        Damage = damage;
        gameObject.SetActive(true);
        transform.position = t.position;
        transform.rotation = t.rotation;

        this.shootFromLayer = shootFromLayer;
    }

    private void FixedUpdate()
    {
        if(_isActive)
        {
            //RigidBody.AddForce(transform.up * Speed);
            RigidBody.velocity = transform.up * Speed;
        }
    }

    private void DeInitBullet()
    {
        gameObject.SetActive(false);
        RigidBody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == this.shootFromLayer)
        {
            return;
        }

        setActiveFalse();

        if (collision.tag == "Border")
        {
        }

        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity !=  null)
        {
            entity.TakeDamage(Damage);
        }
        else
        {
            entity = collision.gameObject.GetComponentInChildren<Entity>();
            if (entity != null)
            {
                entity.TakeDamage(Damage);
            }
        }    
    }

    public virtual void setActiveFalse()
    {
        IsActive = false;
    }

    //todo do something great here
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "BouncyBorder")
    //    {
           
    //    }
    //}
}
