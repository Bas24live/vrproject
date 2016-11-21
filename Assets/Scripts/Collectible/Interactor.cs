using UnityEngine;
using System.Collections;

[RequireComponent(typeof (SphereCollider))]
public class Interactor : Collectible {
    public float decayTime;
    protected bool active = false;

    private Color initialColor;

    protected override void Start()
    {
        base.Start();
        this.initialColor = gameObject.GetComponent<Renderer>().material.color;
    }

    protected override void Update()
    {
        base.Update();
        Decay();
    }

    IEnumerator Decay()
    {
        if (!this.active) yield break;
        yield return new WaitForSeconds(this.decayTime);
        Deactivate();
    }

    public override void Pickup()
    {
        base.Pickup();
        // could potentially deactivate it here, but then you could cheat by just picking it up and dropping it all the time
    }

    public override void Drop(Transform player)
    {
        base.Drop(player);
        if (!this.active)
        {
            this.Activate();
        }
    }

    protected virtual void Activate()
    {
        this.active = true;
    }

    protected virtual void Deactivate()
    {
        this.active = false;
        gameObject.GetComponent<Renderer>().material.color = this.initialColor;
    }
}
