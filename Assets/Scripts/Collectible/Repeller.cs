using UnityEngine;
using System.Collections;

public class Repeller : Interactor {

    // Use this for initialization
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    // when the repeller is active, it must repel enemies
    protected override void Activate()
    {
        base.Activate();
    }

    // deactivation should nullify the repulsion
    protected override void Deactivate()
    {
        base.Deactivate();
    }
}
