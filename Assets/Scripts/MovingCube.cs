using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
    {
    public static MovingCube CurrentCube {  get; private set; }
    public static MovingCube LastCube { get; private set; }

    [SerializeField] private float moveSpeed = 1f;
    bool stop;
    
    
    
    private void OnEnable( )
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        }
        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetRandomColor();
       transform.localScale = new Vector3 (LastCube.transform.localScale.x,transform.localScale.y, LastCube.transform.localScale.z);
    }
    Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f),UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    private void Update()
    {
        if(stop == true)
        {
            return;
        }
        transform.position += transform.forward * Time.deltaTime * moveSpeed;

    }
    public void Stop()
    {
        moveSpeed = 0;
        stop = true;
        float hangOver = transform.position.z - LastCube.transform.position.z;

        if (Mathf.Abs(hangOver) >= LastCube.transform.localScale.z)
        {
            LastCube = null;
            CurrentCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = hangOver > 0 ? 1f : -1f;    
        SplitCubeOnZ(hangOver, direction);
    }
    void SplitCubeOnZ(float hangOver, float direction)
    {
        float newZSize = LastCube.transform.localScale.z- Mathf.Abs(hangOver);
        float fallingBlockSize = transform.localScale.z - newZSize;
        float newZPosition = LastCube.transform.position.z + (hangOver / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x,transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }
    void SpawnDropCube (float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);
    }
}
