using UnityEngine;
using System.Collections;

public class StatePatternBlocker : StatePatternEnemy {

    [HideInInspector] public LookoutState lookoutState;
    [HideInInspector] public BlockerAlertState blockerAlertState;

    void Awake () {

    }

    void Start () {
	
	}
	
	void Update () {
	}
}
