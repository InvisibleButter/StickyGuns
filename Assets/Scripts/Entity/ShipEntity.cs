using System.Collections;
using System.Collections.Generic;
using StickyGuns.Sound;
using UnityEngine;

public class ShipEntity : Entity
{

    private Animator animator;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        OnDeath += ShipDestroy;
    }

    protected virtual void ShipDestroy(Entity entity)
    {
        AudioManager.Instance.Play("bigBang");
        animator.SetTrigger("death");

    }

    public virtual void DestroyAnimationFinished()
    {
        OnAllowToDie?.Invoke();
    }
}
