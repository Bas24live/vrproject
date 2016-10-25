using UnityEngine;
using System.Collections;

public interface IEnemyState {

    void UpdateState();

    void OnTriggerEnter(Collider collider);

    void ToPatrolState();

    void ToChaseState();

    void ToAlertState();

    void ToLasKnownPositionState();

    void ToSearchingState();
}
