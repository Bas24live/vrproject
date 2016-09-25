using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class LookAround : RAINAction
{


    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);


    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        
        ai.Kinematic.Orientation = new Vector3(0, direction(3), 0);

        return ActionResult.SUCCESS;
    }

    private float direction(float t)
    {
        float alpha = .1f;

        return alpha*Mathf.Sin((2*Mathf.PI*t)/Time.deltaTime);
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}