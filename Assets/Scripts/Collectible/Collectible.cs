using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
    protected bool pickedUp = false;

    // Call the event when the player collides with the Collectible
    public event System.Action<Collectible> PickedUpEvent;

    // Call the event when the player drops the Collectible
    public event System.Action<Collectible, Transform> DroppedEvent;

    public virtual void Pickup()
    {
        this.pickedUp = false;
        gameObject.SetActive(false);
        if (PickedUpEvent != null)
        {
            PickedUpEvent(this);
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

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }
}
