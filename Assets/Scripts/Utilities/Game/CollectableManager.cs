using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectableManager : MonoBehaviour
{

    public int numberLamp;
    public int numberAttractors;
    public int numberRepellers;

    public GameObject lampPrefab;
    public GameObject attractorPrefab;
    public GameObject repellerPrefab;

    private GameObject worldContainer;
    private GameObject collectibleContainer;
    private Spawner spawner;
    private List<Collectible> collectibles;
    private string containerName = "Collectibles";

    void Awake()
    {
	    collectibles = new List<Collectible>();
        this.spawner = GetComponent<Spawner>();
        this.worldContainer = GameObject.FindGameObjectWithTag("World");
    }

    public void SpawnCollectibles()
    {
        if (worldContainer.transform.FindChild(containerName))
        {
            Destroy(collectibleContainer);
        }

        collectibleContainer = new GameObject("Collectibles");
        collectibleContainer.transform.SetParent(worldContainer.transform);

        for (int i = 0; i < numberLamp; i++)
        {
            GameObject spawnedLamp;
            spawner.SpawnRandom(lampPrefab, collectibleContainer.transform, out spawnedLamp);
            Lamp lamp = spawnedLamp.gameObject.GetComponent<Lamp>();
            lamp.PickupEvent += OnPickupEvent;
            lamp.DroppedEvent += OnDroppedEvent;
            collectibles.Add(lamp);
        }
        for (int i = 0; i < numberAttractors; i++)
        {
            GameObject spawnedAttractor;
            spawner.SpawnRandom(attractorPrefab, collectibleContainer.transform, out spawnedAttractor);
            Attractor attractor = spawnedAttractor.gameObject.GetComponent<Attractor>();
            attractor.PickupEvent += OnPickupEvent;
            attractor.DroppedEvent += OnDroppedEvent;
            collectibles.Add(attractor);
        }
        for (int i = 0; i < numberRepellers; i++)
        {
            GameObject spawnedRepeller;
            spawner.SpawnRandom(repellerPrefab, collectibleContainer.transform, out spawnedRepeller);
            Repeller repeller = spawnedRepeller.gameObject.GetComponent<Repeller>();
            repeller.PickupEvent += OnPickupEvent;
            repeller.DroppedEvent += OnDroppedEvent;
            collectibles.Add(repeller);
        }
    }

    private void OnDroppedEvent(Collectible collectible, Transform transform)
    {
        throw new NotImplementedException();
    }

    private void OnPickupEvent(Collectible collectible)
    {
        throw new NotImplementedException();
    }
}
