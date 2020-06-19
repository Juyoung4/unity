using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_path : MonoBehaviour
{

    private a_al algorithm; 
    private int[,] rsstate = new int[3, 3]; //state state
    private int[,] gstate = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, -1 } }; //goal state

    void Awake()
    {
        create_random_state();
        set_start_gstate();
        this.algorithm.main();
        path2Text();
    }


    // create random state for init postion
    public void create_random_state()
    {
        List<int> baseline = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, -1 };
        int[] nstate = new int[9]; //shuffle
        int base_len = baseline.Count;

        for (int i = 0; i < base_len; i++)
        {
            int rand = Random.Range(0, baseline.Count);
            nstate[i] = baseline[rand];
            baseline.RemoveAt(rand);
        }

        for (int i = 0, j = 0; i < this.rsstate.GetLength(0); i++)
        {
            for (int k = 0; k < this.rsstate.GetLength(1); k++, j++)
            {
                this.rsstate[i, k] = nstate[j];
            }
        }

        Debug.Log("@@start@@");
        print_state(this.rsstate);
    }


    // start and goal state set
    public void set_start_gstate()
    {
        this.algorithm = gameObject.GetComponent<a_al>();
        this.algorithm.rsstate = this.rsstate;
        this.algorithm.gstate = this.gstate;
    }


    // print state(path)
    public string print_state(int[,] st)
    {
        string s = "\n\t";

        for (int i = 0; i < st.GetLength(0); i++)
        {
            for (int j = 0; j < st.GetLength(1); j++)
            {
                s += st[i, j] + "\t";
            }
            s += "\n\t";
        }
        s += "";
        s += "  | ";
        s += " \\\'/ \n";
        Debug.Log(s);
        return s;
    }

    // path convert to txt file
    public void path2Text()
    {
        string savePath = @"C:\Users\kyoun\Desktop\4-1\게임프로그래밍\8-Puzzle Using A Star\result.txt";
        string textValue = "";
        List<int[,]> path = algorithm.path;

        Debug.Log("all path [start~goal]");
        for (int i = 0; i < path.Count; i++)
        {
            textValue += " all path[start~goal] : " + (i + 1).ToString();
            if (i == 0 || i == (path.Count - 1))
            {
                if (i == 0)
                {
                    textValue += "\t(start path)";
                }
                else
                {
                    textValue += "\t(goal path)";
                }
            }
            textValue += print_state(path[i]) + "\n";
        }

        System.IO.File.WriteAllText(savePath, textValue);
    }

    // cubestate needs start state
    public int[,] get_start_state()
    {
        return this.rsstate;
    }

    // final path save
    public List<int[]> SequenceOfpath()
    {
        List<int[]> final_path = new List<int[]>();

        foreach (int[,] p in algorithm.path)
        {
            final_path.Add(algorithm.find_blank_pos(p));
        }
        foreach(int[] n in final_path)
        {
            Debug.Log(n[0] + " , " + n[1]);
        }

        return final_path;
    }
}