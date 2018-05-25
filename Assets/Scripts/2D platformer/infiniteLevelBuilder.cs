using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(objectPooling))]
public class infiniteLevelBuilder : MonoBehaviour
{

    private objectPooling objPooler;

    private Transform cam;
    private float spawnAtX = 0f;

    [SerializeField]
    private float tileLength = 5f;
    public int amountOfTilesToSpawn = 6;

    [SerializeField]
    private float safeZone = 11f;
    private int lastPrefabSpawned = 0;

    //objectPooler objPooler;

    // Use this for initialization
    void Start()
    {
        objPooler = GetComponent<objectPooling>();

        cam = Camera.main.transform;

        for (int i = 0; i < amountOfTilesToSpawn; i++)
        {
            spawnTile();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            if (cam.position.x - safeZone > (spawnAtX - amountOfTilesToSpawn * tileLength))
            {
                spawnTile();
                deleteTile();
            }
        }

    }

    private void spawnTile()
    {

        GameObject go = objPooler.getNextItemInPool();

        go.transform.position = Vector3.right * spawnAtX;
        spawnAtX += tileLength;
    }

    private void deleteTile()
    {
        objPooler.stopUse();
    }

}
