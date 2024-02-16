using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatGenerator : MonoBehaviour
{

    [SerializeField] public ObjectPooling meatPool;

    [SerializeField] public float distanceBetweenMeat;

    public void SpawnMeat(Vector3 startPosition)
    {
        GameObject meat1 = meatPool.GetPoolObject();
        meat1.transform.position = startPosition;
        meat1.SetActive(true);
        
        GameObject meat2 = meatPool.GetPoolObject();
        meat2.transform.position = new Vector3(startPosition.x - distanceBetweenMeat, startPosition.y, startPosition.z);
        meat2.SetActive(true);
        
        GameObject meat3 = meatPool.GetPoolObject();
        meat3.transform.position = new Vector3(startPosition.x + distanceBetweenMeat, startPosition.y, startPosition.z);
        meat3.SetActive(true);
    }
    
}
