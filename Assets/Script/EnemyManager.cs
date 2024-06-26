using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Inscribed")]
    public float spawnDistance = 10f;
    public int maxEnemies = -1;
    public Enemy[] enemyPrefabs;
    private Field parentField;

    [Header("Dynamic")]
    public bool canSpawn = true;

    //making these separate such that other scripts can mess with it
    public float a = 0.00004f;
    public float b = 0.05f;
    public float c = 0.1f;
    public float enemiesPerSecond
    {
        get {

            float val = Mathf.Pow(a * Time.timeSinceLevelLoad, 2) + b * Time.timeSinceLevelLoad + c;
            //float val = 0.05f * Time.timeSinceLevelLoad + 0.5f;
            float valSkewed = Random.Range(val * 0.9f, val * 1.1f);
            return valSkewed;
        }
    }
    public float spawnInterval
    {   
        get {
            float rand = Random.Range(0.95f, 1.05f);
            return (1 / (enemiesPerSecond*rand));
        }
    }

    private void Start()
    {
        parentField = GetComponentInParent<Field>();
        Invoke(nameof(SpawnEnemy), 0.5f);
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

        //TODO- remove hardcoded z axis offset
        createdEnemy.transform.position = new(spawnPoint.x, spawnPoint.y, parentField.player.transform.position.z);
        createdEnemy.transform.rotation = Quaternion.Euler(0, 90, -90);

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
