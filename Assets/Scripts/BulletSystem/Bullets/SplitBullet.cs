using UnityEngine;

public class SplitBullet : Bullet
{
    public float TimeUntilSplit = 0.2f;
    public float SplitAmount = 5;
    public float SplitAngle = 45;

    private float _remaingTimeUntilSplit;
    private bool _splitted;

    public override Type BulletType => Type.Split;

    public override void InitBullet(Transform t, int damage)
    {
        base.InitBullet(t, damage);

        _remaingTimeUntilSplit = TimeUntilSplit;
        _splitted = false;
    }

    private void Update()
    {
        if(_splitted)
        { 
            return;
        }

        if(_remaingTimeUntilSplit <= 0)
        {
            _splitted = true;
            float angle = SplitAngle / SplitAmount;
            float start = Vector2.Angle(Vector2.up, transform.up) - (SplitAngle / 2);

            for (int i = 0; i < SplitAmount; i++)
            {
                Transform t = transform;
                t.eulerAngles = new Vector3(0, 0, start + angle * (i + 1));
                BulletSpawner.Instance.SpawnBullet(t, Damage, Type.Standard);
                IsActive = false;
            }
        }
        else
        {
            _remaingTimeUntilSplit -= Time.deltaTime;
        }
    }
}
