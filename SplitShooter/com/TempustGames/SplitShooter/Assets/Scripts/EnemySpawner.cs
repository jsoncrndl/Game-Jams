using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    [Tooltip("The maximum number of objects this prefab can spawn. Leave zero for no limit")]
    public List<Bounds> spawnArea;

    public List<GameObject> spawnedObjects;

    public Mesh cube;

    private void Start()
    {
        spawnedObjects = new List<GameObject>();
    }

    private void OnDrawGizmos()
    {
        foreach (Bounds bounds in spawnArea)
        {
            Gizmos.DrawWireMesh(cube, bounds.center, Quaternion.identity, bounds.extents * 2);
        }
    }

    public void Spawn(int numSpawns, Vector3 target, int moveSpeed)
    {
        for (int i = 0; i < numSpawns; i++)
        {
            int prefabIndex = Random.Range(0, prefabs.Count);

            Bounds area = spawnArea[Random.Range(0, spawnArea.Count)];
            Vector3 pos = new Vector3(Random.Range(area.min.x, area.max.x), Random.Range(area.min.y, area.max.y), Random.Range(area.min.z, area.max.z));

            GameObject newProj = Instantiate(prefabs[prefabIndex], transform.position + pos, Quaternion.LookRotation(Vector3.forward, target - (transform.position + pos)), transform);
            newProj.GetComponent<Projectile>().fireSpeed = moveSpeed;
            spawnedObjects.Add(newProj);
        }
    }

    public void DestroyAll()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            Destroy(spawnedObjects[i]);
        }
        spawnedObjects.Clear();
    }
}