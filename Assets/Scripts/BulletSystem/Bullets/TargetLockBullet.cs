using UnityEngine;

public class TargetLockBullet : Bullet
{
    public override Type BulletType => Type.TargetLock;

    public override void InitBullet(Transform t, int damage, int layer)
    {
        //todo just for testing 
        Transform t2 = GameObject.FindGameObjectWithTag("Enemy").transform;
        Vector3 dir = (t.position - t2.position).normalized;
        t.localRotation = Quaternion.Euler(dir);
        base.InitBullet(t, damage, layer);
    }
}
