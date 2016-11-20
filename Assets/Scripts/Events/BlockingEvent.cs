using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BlockingEvent : MonoBehaviour {
    UnityAction blockingStateListener;
    StatePatternBlocker enemy;

    void Awake() {
        blockingStateListener = new UnityAction(Block);
        enemy = GetComponent<StatePatternBlocker>();
    }

    void OnEnable() {
        EventManager.StartListening("Block", Block);
    }

    void OnDisable() {
        EventManager.StopListening("Block", Block);
    }

    void Block() {
        //enemy.Block();
    }
}