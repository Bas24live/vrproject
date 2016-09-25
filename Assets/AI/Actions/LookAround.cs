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
		float t = 0.0f;
		float T = 100.0f;
		while (t < T) {
			ai.Kinematic.Orientation = new Vector3 (0, direction (t), 0);
			t += Time.deltaTime;
		}

        return ActionResult.SUCCESS;
    }

    private float direction(float t)
    {
        float alpha = .1f;

		return alpha*Mathf.Sin(2 * Mathf.PI * t);
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}