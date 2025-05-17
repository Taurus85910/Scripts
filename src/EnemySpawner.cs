using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10; 
    [SerializeField] private float spawnRadius = 5f; 
    [SerializeField] private LayerMask surfaceLayer; 
    [SerializeField] private float spawnInterval = 2f;
    
    private List<GameObject> objectPool;

    private void Start()
    {
        
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            
            GameObject obj = Instantiate(objectPrefab,gameObject.transform);
            obj.SetActive(false);
            objectPool.Add(obj);
        }

        
        StartCoroutine(SpawnObjectsRoutine());
    }

    
    private IEnumerator SpawnObjectsRoutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(spawnInterval); 
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        
        GameObject objToSpawn = objectPool.Find(obj => !obj.activeSelf);
        
        if (objToSpawn is not null)
        {
            
            Vector3 randomPoint = Random.insideUnitSphere * spawnRadius;
            randomPoint += transform.position;

            
            RaycastHit hit;
            if (Physics.Raycast(randomPoint, -transform.up, out hit, Mathf.Infinity, surfaceLayer))
            {
                objToSpawn.SetActive(true);
                objToSpawn.transform.position = hit.point;
                objToSpawn.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }

}
