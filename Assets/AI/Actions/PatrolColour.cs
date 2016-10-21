using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class PatrolColour : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        
        GameObject enemy = ai.WorkingMemory.GetItem<GameObject>("Seeker");

        Light vision = enemy.GetComponent(typeof(Light)) as Light;
        vision.color = new Color(0, 255, 86);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}