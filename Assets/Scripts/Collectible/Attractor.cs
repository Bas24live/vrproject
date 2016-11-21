using UnityEngine;
using System.Collections;

public class Attractor : Interactor {

	// Use this for initialization
	protected override void Start () {
	
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}

    // when the attractor is active, it must attract enemies
    protected override void Activate()
    {
        base.Activate();
    }

    // deactivation should nullify the attraction
    protected override void Deactivate()
    {
        base.Deactivate();
    }
}
