using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    public GameObject cubestateObj;

    public Vector3 start_pos;
    private float speed = 2.0f;
    private Transform c_pos;
    private cubestate cube_state;
    public bool move_ok = false;
    [HideInInspector]
    public float mx, mz;
    [HideInInspector]
    public Vector3 goal_pos; 
    [HideInInspector]

    void Start()
    {
        c_pos = gameObject.GetComponent<Transform>();
        cube_state = cubestateObj.GetComponent<cubestate>();

    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        start_pos = transform.localPosition;
        if (!move_ok)
        {
            c_pos.localPosition = Vector3.MoveTowards(start_pos, goal_pos, step);
        }
    }
}

