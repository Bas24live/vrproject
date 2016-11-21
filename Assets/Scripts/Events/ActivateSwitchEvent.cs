using UnityEngine;
using UnityEngine.Events;

public class ActivateSwitchEvent : MonoBehaviour {

    UnityAction activateSwitchListener;
    SwitchSystem switchSystem;

    void Awake() {
        activateSwitchListener = new UnityAction(SwitchActivateded);
        switchSystem = GetComponent<SwitchSystem>();
    }

    void OnEnable() {
        EventManager.StartListening("SwitchActivated", SwitchActivateded);

    }

    void OnDisable() {
        EventManager.StopListening("SwitchActivated", SwitchActivateded);
    }

    void SwitchActivateded() {
        switchSystem.ActivateSwitch();
    }
}
