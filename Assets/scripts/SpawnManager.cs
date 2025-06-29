using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject my_Player;

    public List<ResourceClass> allResources;

    private Vector2 spawnRangeX = new Vector2(-130, 130);      //map borders 
    private Vector2 spawnRangeZ = new Vector2(-130, 130);

    public float minDistanceBetweenObjects = 1.5f;

    private List<List<GameObject>> allPoolObjects;         //for object pooling mechanic 

    void Start()
    {
        allPoolObjects = new List<List<GameObject>>();          

        for (int i = 0; i < allResources.Count; i++)       // iterate over all resources 
        {
            var resource = allResources[i];                
            allPoolObjects.Add(new List<GameObject>());    // add it to the list for object pooling          

            for (int j = 0; j < resource.maxInGame; j++)   // first spawn when game starts and pooling objects
            {
                Vector3 spawnPosition = GetPosition();
                GameObject obj = Instantiate(resource.resourcePrefab, spawnPosition, Quaternion.identity);
                obj.SetActive(true);
                allPoolObjects[i].Add(obj);

                CollectingMaterials collectingMaterials = obj.GetComponent<CollectingMaterials>();
                int index = i;

                if (collectingMaterials != null)
                {
                    obj.GetComponent<CollectingMaterials>().my_Player = my_Player;
                    collectingMaterials.ResetResource(resource.defaultObjectLife);  //must reset so object_life always correct
                    collectingMaterials.OnCollectedCallback = () => StartCoroutine(RespawnAfterDelay(index, obj, resource.respawnTime));
                
                }
                else
                {
                    Debug.LogWarning($"Prefab {resource.resourcePrefab.name} nie ma komponentu CollectingMaterials!");
                }

            }
        }
    }

    Vector3 GetPosition()                            
    {
        for(int a = 0; a < 15; a++)                                 
        {
            float x = UnityEngine.Random.Range(spawnRangeX.x, spawnRangeX.y);
            float z = UnityEngine.Random.Range(spawnRangeZ.x, spawnRangeZ.y);
            Vector3 newPos = new Vector3(x, 0, z);

            Collider[] colliders = Physics.OverlapSphere(newPos, minDistanceBetweenObjects);
            if(colliders.Length ==0)
            {
                return newPos;
            }
        }
        return Vector3.zero;
    }

    IEnumerator RespawnAfterDelay(int index, GameObject obj, float delay)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(delay);

        Vector3 newPos = GetPosition();
        obj.transform.position = newPos;

        var collectingMaterials = obj.GetComponent<CollectingMaterials>();
        if (collectingMaterials != null)
        {
            collectingMaterials.ResetResource(allResources[index].defaultObjectLife);
        }

        obj.SetActive(true);
    }
}
