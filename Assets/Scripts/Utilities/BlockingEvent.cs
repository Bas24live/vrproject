using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BlockingEvent : MonoBehaviour {
    UnityAction blockingStateListener;
    StatePatternEnemy enemy;

    void Awake() {
        blockingStateListener = new UnityAction(Block);
        enemy = this.GetComponent<StatePatternEnemy>();
    }

    void OnEnable() {
        EventManager.StartListening("Block", Block);
    }

    void OnDisable() {
        EventManager.StopListening("Block", Block);
    }

    void Block() {
        enemy.Block();
    }
}