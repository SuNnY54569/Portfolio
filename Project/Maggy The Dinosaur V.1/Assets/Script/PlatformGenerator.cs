using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public GameObject[] thePlatform;
    [SerializeField] public Transform generationPoint;
    [SerializeField] public float distanceBetween;

    [SerializeField] private float platformWidth;

    [SerializeField] private float distanceBetweenMin;
    [SerializeField] private float distanceBetweenMax;

    public ObjectPooling[] theObjectPools;
    
    //[SerializeField] public GameObject[] thePlatforms;
    [SerializeField] private int platformSelector;
    [SerializeField] private float[] platformWidths;

    [SerializeField] private float minHeight;
    [SerializeField] public Transform maxHeightPoint;
    [SerializeField] private float maxHeight;
    [SerializeField] public float maxHeightChange;
    [SerializeField] private float heightChange;

    [SerializeField] private MeatGenerator theMeatGenerator;
    [SerializeField] public float randomMeatThershold;

    [SerializeField] public float randomBoneThreshold;
    [SerializeField] public ObjectPooling bonePool;

    [SerializeField] public float powerUpHeight;
    [SerializeField] public ObjectPooling powerUpPool;
    [SerializeField] public float powerUpThreshold;
        
    void Start()
    {
        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theMeatGenerator = FindObjectOfType<MeatGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            
            platformSelector = Random.Range(0, theObjectPools.Length);
            
            transform.position = new Vector3(transform.position.x + platformWidths[platformSelector] + distanceBetween,
                                                                                      heightChange, transform.position.z);
            
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            if (Random.Range(0f, 100f) < powerUpThreshold)
            {
                GameObject newPowerUp = powerUpPool.GetPoolObject();

                newPowerUp.transform.position =
                    transform.position + new Vector3(distanceBetween / 2f, Random.Range(powerUpHeight/2f, powerUpHeight), 0f);
                newPowerUp.SetActive(true);
                newPowerUp.transform.rotation = transform.rotation;
            }
            
            GameObject newPlatform = theObjectPools[platformSelector].GetPoolObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0f, 100f) < randomMeatThershold)
            {
                theMeatGenerator.SpawnMeat(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomBoneThreshold)
            {
                GameObject newBone = bonePool.GetPoolObject();

                float currentPlatformWidth = platformWidths[platformSelector];
                float newSpikeXPosition = Random.Range(((currentPlatformWidth / 2) * -1) + 1f, (currentPlatformWidth / 2) - 1f);
                Vector3 newPosition = new Vector3(newSpikeXPosition, .5f, 0f);
                newBone.transform.position = transform.position + newPosition;
                newBone.transform.rotation = transform.rotation;
                newBone.SetActive(true);
            }            
            
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), heightChange, transform.position.z);
        }
    }
}
