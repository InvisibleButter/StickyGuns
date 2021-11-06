using UnityEngine;

public class SplitBullet : Bullet
{
    public float TimeUntilSplit = 0.2f;
    public float SplitAmount = 1;

    private float _remaingTimeUntilSplit;

    public override Type BulletType => Type.Split;

    public override void InitBullet(Transform t, int damage)
    {
        base.InitBullet(t, damage);
        _remaingTimeUntilSplit = TimeUntilSplit;
    }

    private void Update()
    {
        if(_remaingTimeUntilSplit <= 0)
        {
            int angle = Random.Range(-30, 30);
            Transform t = transform;
            t.Rotate(0f, 0f, angle, Space.Self);
            BulletSpawner.Instance.SpawnBullet(t, Damage, Type.Standard);
        }
        else
        {
            _remaingTimeUntilSplit -= Time.deltaTime;
        }
    }
}
