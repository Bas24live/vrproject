using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    bool active;

    void Awake() {
        active = false;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player"))
            Activate();
    }

    public void Activate() {
        if (!active) {
            active = true;
            EventManager.TriggerEvent("SwitchActivated");

            Renderer switchRenderer = GetComponent<Renderer>();
            ParticleSystem particleSystem =  GetComponentInChildren<ParticleSystem>();
            Renderer particelSysRenderer = particleSystem.GetComponent<Renderer>();

            switchRenderer.material.SetColor("_EmissionColor", Color.green);
            particelSysRenderer.material.SetColor("_EmissionColor", Color.green);
        }
    }

}
