using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
    public bool pickedUp;

    // Call the event when the player collides with the Collectible
    public event System.Action<Collectible> PickupEvent;

    // Call the event when the player drops the Collectible
    public event System.Action<Collectible, Transform> DroppedEvent;

    public virtual void Pickup()
    {
        this.pickedUp = false;
        gameObject.SetActive(false);
        if (PickupEvent != null)
        {
            PickupEvent(this);
        }
    }

    public virtual void Drop(Transform player)
    {
        this.pickedUp = true;
        gameObject.SetActive(true);
        if (DroppedEvent != null)
        {
            DroppedEvent(this, player);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            Pickup();
    }

    protected virtual void Awake()
    {
        this.pickedUp = false;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }
}
