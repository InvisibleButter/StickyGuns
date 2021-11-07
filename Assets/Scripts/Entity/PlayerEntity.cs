using StickyGuns.Sound;
using UnityEngine;

public class PlayerEntity : ShipEntity
{
    protected void Start()
    {
        base.Start();
    }

    protected override void ShipDestroy(Entity entity)
    {
        base.ShipDestroy(entity);
        foreach(Weapon weapon in WeaponManager.Instance.Weapons.ToArray())
        {
            WeaponManager.Instance.DeRegister(weapon);
        }
    }


    public override void DestroyAnimationFinished()
    {
        base.DestroyAnimationFinished();
    }
}
