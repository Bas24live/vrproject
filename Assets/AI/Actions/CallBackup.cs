using RAIN.Action;
using RAIN.Core;
using RAIN.Entities.Aspects;
using UnityEngine;

[RAINAction]
public class CallBackup : RAINAction
{
    public CallBackup()
    {
        actionName = "CallBackup";
    }

    public override ActionResult Execute(AI ai)
    {
        ai.WorkingMemory.SetItem<bool>("searching", true);

        return ActionResult.SUCCESS;
    }
}