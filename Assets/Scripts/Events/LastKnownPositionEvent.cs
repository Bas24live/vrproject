using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LastKnownPositionEvent : MonoBehaviour
{
    UnityAction lastKnownPositionListener;
    StatePatternEnemy enemy;

    void Awake() {
        lastKnownPositionListener = new UnityAction(LastKnownPosition);
        enemy = this.GetComponent<StatePatternEnemy>();
    }

    void OnEnable() {
        EventManager.StartListening("LastKnownPosition", LastKnownPosition);
        
    }

    void OnDisable() {
        EventManager.StopListening("LastKnownPosition", LastKnownPosition);
    }

    void LastKnownPosition() {
        enemy.LastKnownPosition();
    }
}