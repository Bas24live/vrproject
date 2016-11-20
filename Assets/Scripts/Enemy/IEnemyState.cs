using UnityEngine;
using System.Collections;

public interface IEnemyState {

    void UpdateState ();

    void OnTriggerEnter (Collider collider);

    void ToAlertState ();

    void ToLasKnownPositionState ();

    void ToSearchingState ();

    void ToDeathState ();
}
