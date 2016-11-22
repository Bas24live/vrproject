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

    private List<Collectible> collectibles;

	// Use this for initialization
	void Start ()
	{
	    collectibles = new List<Collectible>();
	    SpawnCollectibles();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void SpawnCollectibles()
    {
        for (int i = 0; i < numberLamp; i++)
        {

        }
    }
}
