using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class LookAround : RAINAction
{
    public LookAround()
    {
        actionName = "lookAround";
    }

    public override ActionResult Execute(AI ai)
    {
        // get the unit vector one value ahead of the ai
        Vector3 ahead = ai.Kinematic.Position + new Vector3(0, 0, 1.0f);
        Vector3 leftLook = rotatePointAroundPivot(ahead, ai.Kinematic.Position, new Vector3(0, 60, 0));
        Vector3 rightLook = rotatePointAroundPivot(ahead, ai.Kinematic.Position, new Vector3(0, 300, 0));

        ai.WorkingMemory.SetItem<Vector3>("leftLook", leftLook);
        ai.WorkingMemory.SetItem<Vector3>("rightLook", rightLook);
        
        return ActionResult.SUCCESS;
    }

    private Vector3 rotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 eulerAngles)
    {
        Vector3 direction = point - pivot;
        Vector3 newDirection = Quaternion.Euler(eulerAngles) * direction;
        return newDirection + pivot;
    }
 
}