using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Inscribed")]
    public float spawnInterval = 2.5f;
    public float spawnDistance = 10f;
    public int maxEnemies = -1;
    public Enemy[] enemyPrefabs;
    private Field parentField;

    [Header("Dynamic")]
    public bool canSpawn = true;

   

    private void Start()
    {
        parentField = GetComponentInParent<Field>();
        Invoke(nameof(SpawnEnemy), spawnInterval);
    }

    public Vector3 GetPointOnSquareEdge(float degrees)
    {
        float radians = Mathf.Deg2Rad * degrees;
        float tangent = Mathf.Tan(radians);

        float x, y;

        // right edge
        if (degrees <= 45 || degrees > 315)
        {
            x = spawnDistance;
            y = tangent * spawnDistance;
        }
        // top edge
        else if (degrees > 45 && degrees <= 135)
        {
            x = -spawnDistance / tangent;
            y = spawnDistance;
        }
        // left edge
        else if (degrees > 135 && degrees <= 225)
        {
            x = -spawnDistance;
            y = -tangent * spawnDistance;
        }
        //bottom edge
        else
        {
            x = spawnDistance / tangent;
            y = -spawnDistance;
        }

        return new Vector3(x, y, 0);
    }

    void SpawnEnemy()
    {
        if ( maxEnemies != -1 && parentField.enemyList.Count >= maxEnemies)
        {
            Invoke(nameof(SpawnEnemy), spawnInterval);
            return;
        }

        if (!canSpawn)
        {
            Invoke(nameof(SpawnEnemy), spawnInterval);
            return;
        }

        Vector3 spawnPoint = this.transform.position + GetPointOnSquareEdge(Random.value * 360);
        Enemy createdEnemy = Instantiate(GetRandomEnemy(), parentField.transform);
        createdEnemy.transform.position = spawnPoint;

        parentField.enemyList.Add(createdEnemy);
        
        Invoke(nameof(SpawnEnemy), spawnInterval);
    }

    private void OnEnable()
    {
        canSpawn = true;
    }

    private void OnDisable()
    {
        canSpawn = false;
    }

    Enemy GetRandomEnemy ()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }
}
