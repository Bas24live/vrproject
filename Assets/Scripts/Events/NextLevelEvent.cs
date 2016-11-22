using UnityEngine;
using UnityEngine.Events;

public class NextLevelEvent : MonoBehaviour {

    UnityAction nextLevelListener;
    GameStateManager gameStateManager;

    void Awake() {
        nextLevelListener = new UnityAction(NextLevel);
        gameStateManager = GetComponent<GameStateManager>();
    }

    void OnEnable() {
        EventManager.StartListening("NextLevel", NextLevel);

    }

    void OnDisable() {
        EventManager.StopListening("NextLevel", NextLevel);
    }

    void NextLevel() {
        gameStateManager.NextLevel();
    }
}
