using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public List<GameObject> spawnPoints, spawnPointsTwo;
    public List<GameObject> prefabs;
    public GameObject MParticle, FParticle;
    void spawnOBJ(GameObject spawnPoint, int firstOrLast)
    {
        if (firstOrLast == 0)
        {
            Instantiate(prefabs[0], spawnPoint.transform.position, spawnPoint.transform.rotation);
            Instantiate(MParticle, spawnPoint.transform.position-new Vector3(0,0.35f,0),MParticle.transform.rotation);
        }
        else
        {
            Instantiate(prefabs[1], spawnPoint.transform.position, spawnPoint.transform.rotation);
            Instantiate(FParticle, spawnPoint.transform.position-new Vector3(0,0.35f,0), FParticle.transform.rotation);
        }

    }
    void chooseFromList()
    {
        foreach (var item in spawnPoints)
        {
            spawnOBJ(item, 0);
        }
        foreach (var item in spawnPointsTwo)
        {
            spawnOBJ(item, 1);
        }
    }
    private void Start()
    {
        chooseFromList();
    }
}
