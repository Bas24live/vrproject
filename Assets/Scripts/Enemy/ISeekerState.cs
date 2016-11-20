using UnityEngine;
using System.Collections;

public interface ISeekerState : IEnemyState {

    void ToPatrolState ();

    void ToChaseState ();
}
