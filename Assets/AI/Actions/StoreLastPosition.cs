using RAIN.Action;
using RAIN.Core;
using RAIN.Entities.Aspects;
using UnityEngine;

[RAINAction]
public class StoreLastPosition : RAINAction
{
    public StoreLastPosition()
    {
        actionName = "StoreLastPosition";
    }

    public override ActionResult Execute(AI ai)
    {
        // If you use the Aspect Variable property
        //RAINAspect tPlayerAspect = ai.WorkingMemory.GetItem<RAINAspect>("playerAspect");
        // If you use the Mount Point Variable property
        //Transform tPlayerMount = ai.WorkingMemory.GetItem<Transform>("playerMount");
        // If you use the Form Variable property
        GameObject tPlayerForm = ai.WorkingMemory.GetItem<GameObject>("found");

        // Store the last position in our memory for use later
        ai.WorkingMemory.SetItem<Vector3>("lastKnownPos", tPlayerForm.transform.position);

        return ActionResult.SUCCESS;
    }
}