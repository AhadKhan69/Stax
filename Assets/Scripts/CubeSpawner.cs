using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
   [SerializeField] private MovingCube cubePrefab;

    public void SpawnCube()
    {
        var cube = Instantiate(cubePrefab);

        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("Start"))
        {
            cube.transform.position = new Vector3(transform.position.x, MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y, transform.position.z);
        }
        else
        {
            cube.transform.position = transform.position;
        }
            
    }
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireCube(transform.position,cubePrefab.transform.localScale);

    //}
}
