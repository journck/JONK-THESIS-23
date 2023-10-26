using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float spawnInterval = 2.5f;

    // this is the 'radius' of the square ( i.e. half of a sidelength )
    public float spawnDistance = 10f;

    private Field parentField;

    //TODO - should make this an array that I can randomly choose from
    public Enemy enemyPrefab;

    private void Start()
    {
        Invoke(nameof(SpawnEnemy), spawnInterval);
        parentField = GetComponentInParent<Field>();
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

        Vector3 spawnPoint = this.transform.position + GetPointOnSquareEdge(Random.value * 360);
        Enemy createdEnemy = Instantiate(enemyPrefab, parentField.transform);
        createdEnemy.transform.position = spawnPoint;

        parentField.enemyList.Add(createdEnemy);
        
        Invoke(nameof(SpawnEnemy), spawnInterval);
    }
}
