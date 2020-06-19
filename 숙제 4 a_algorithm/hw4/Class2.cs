using UnityEngine;
using System.Collections.Generic;

public class Slide : MonoBehaviour
{
    // 변수
    public int xDim;
    public int yDim;
    public GameObject prefab;
    public Camera camera;
    private float timer = 0f;
    private float stopTimer = 0;

    private List<List<GameObject>> tiles = new List<List<GameObject>>();

    // Use this for initialization
    void Start()
    {
        camera.transform.position = new Vector3((xDim - 1) / 2.0f, (xDim + yDim) / 2.0f, -((yDim - 1) / 2.0f));

        GameObject clone = prefab;
        int counter = 0;
    }
}