using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public float Speed = 20f;
    public int Damage = 1;

    private bool _isActive;
    private Transform _weaponTrans;

    public enum Type
    {
        Standard,
        Split,
        TargetLock
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

    public virtual void InitBullet(Transform t, int damage)
    {
        _weaponTrans = t;
        Damage = damage;
        gameObject.SetActive(true);
        transform.position = t.position;
    }

    private void FixedUpdate()
    {
        if(_isActive)
        {
            RigidBody.AddForce(_weaponTrans.up * Speed);
        }
    }

    private void DeInitBullet()
    {
        gameObject.SetActive(false);
        RigidBody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border")
        {
            IsActive = false;
        }

        IEntity entity = collision.gameObject.GetComponent<IEntity>();
        if (entity !=  null)
        {
            entity.TakeDamage(Damage);

            IsActive = false;
        }
    }

    //todo do something great here
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "BouncyBorder")
    //    {
           
    //    }
    //}
}
