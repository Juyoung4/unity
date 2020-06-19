using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubestate : MonoBehaviour {

    public GameObject cubes;
    public GameObject gameManager;
    private int[,] state;
    private float ratio = 2.250f;
    private GameObject[,] cube_pos;
    private a_path states;
    private List<int[]> final_path;
    

    void Start()
    {
        states = gameManager.GetComponent<a_path>();
        this.state = states.get_start_state();

        set_cubes();
        initposition();
        this.final_path = states.SequenceOfpath();

        StartCoroutine("move");
    }

    //cubes set init postion
    public void initposition()
    {
        for (int i = 0; i < this.cube_pos.GetLength(0); i++)
        {
            for (int j = 0; j < this.cube_pos.GetLength(1); j++)
            {
                if (this.cube_pos[i, j] == null)
                {
                    continue;
                }
                else
                {
                    cube_pos[i, j].GetComponent<Transform>().position = new Vector3((j - 1) * ratio, cube_pos[i, j].GetComponent<Transform>().position.y, (1 - i) * ratio);
                }
            }
        }
    }

    // cube set with each state(postion)
    public void set_cubes()
    {
        this.cube_pos = new GameObject[this.state.GetLength(0), this.state.GetLength(1)];

        for (int i = 0; i < this.state.GetLength(0); i++)
        {
            for (int j = 0; j < this.state.GetLength(1); j++)
            {
                if (this.state[i, j] == -1)
                {
                    cube_pos[i, j] = null;
                }
                else
                {
                    cube_pos[i, j] = cubes.transform.GetChild(this.state[i, j] - 1).gameObject;
                    //Debug.Log(cube_pos[i, j].name);
                }
            }
        }
    }

    // cube moving with Coroutine
    IEnumerator move()
    {
        move moving;

        for (int i = 0; i < final_path.Count - 1; i++)
        {
            //Debug.Log("check");
            moving = cube_pos[final_path[i + 1][1], final_path[i + 1][0]].GetComponent<move>();
            moving.mx = (1 - final_path[i][0]) * ratio;
            moving.mz = (final_path[i][1] - 1) * ratio;
            moving.goal_pos = new Vector3(moving.mx, 0, moving.mz);
            moving.move_ok = false;
            cube_pos[final_path[i][1], final_path[i][0]] = cube_pos[final_path[i + 1][1], final_path[i + 1][0]];
            cube_pos[final_path[i + 1][1], final_path[i + 1][0]] = null;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
