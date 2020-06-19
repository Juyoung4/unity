using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_al : MonoBehaviour
{
    public int[,] rsstate; 
    public int[,] gstate;
    private List<Node> open;
    private List<Node> close;
    public List<int[,]> path;

    
    public class Node // Node class
    {
        public int[,] cstate;
        public int fval;
        public int gval;
        public int hval;
        public int[,] parent;

        public Node(int[,] cur, int f, int g, int h, int[,] parent)
        {
            this.cstate = cur;
            this.fval = f;
            this.gval = g;
            this.hval = h;
            this.parent = parent;
        }
    }



    // a star glgorithm funtion
    public void a_star_al()
    {

        bool hasnode = false;//hasNode
        Node lowest = null;
        List<Node> poss_node = new List<Node>(); 

        for (int i = 0; i < 10000; i++)
        {
            
            lowest = lowest_node();
            this.open.Remove(lowest);

            
            if (equal_check(lowest.cstate, this.gstate))
            {
                this.close.Add(lowest);
                break;
            }

            //find next state for lowest
            poss_node = find_next_state(lowest);

        
            foreach (Node n in poss_node)
            {

                // check same state in open
                foreach (Node o in this.open)
                {
                    if (equal_check(n.cstate, o.cstate))
                    {
                        hasnode = true;
                        if (n.gval < o.gval)
                        {

                            this.open.Remove(o);
                            this.open.Add(n);
                            break;
                        }
                        else
                        {

                            break;
                        }
                    }
                }

                if (!hasnode)
                {
                    foreach (Node c in this.close)
                    {
                        if (equal_check(n.cstate, c.cstate))
                        {
                            hasnode = true;
                            if (n.gval < c.gval)
                            {
                                this.close.Remove(c);
                                this.open.Add(n);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if (!hasnode)
                {
                    this.open.Add(n);
                }
                hasnode = false;
            }
            this.close.Add(lowest);
        }

        if (!equal_check(lowest.cstate, this.gstate))
        {
            Debug.Log(" path does not find for 10000");
        }
    }

    // solve lowest value in f
    public Node lowest_node()
    {
        Node lowest = null;

        foreach (Node n in this.open)
        {
            if (lowest != null)
            {
                if (lowest.fval > n.fval)
                {
                    lowest = n;
                }
            }
            else
            {
                lowest = n;
            }
        }

        return lowest;
    }

    // check equal to two array 
    public bool equal_check(int[,] state1, int[,] state2)
    {
        for (int i = 0; i < state1.GetLength(0); i++)
        {
            for (int j = 0; j < state1.GetLength(1); j++)
            {
                if (state1[i, j] != state2[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    //the 4 directions[up, down, left, right] respectively
    public List<Node> find_next_state(Node node)
    {
        List<Node> move_check = new List<Node>(); //res
        int[] cur_pos;//cur_position;
        int[,] nstate; //new_state
        int nhval, ngval; //new_hVal, new_gVal
        int temp;

        cur_pos = find_blank_pos(node.cstate);

        // direction[right]
        if (cur_pos[0] + 1 < 3)
        {
            nstate = (int[,])node.cstate.Clone();
            temp = nstate[cur_pos[1], cur_pos[0] + 1];
            nstate[cur_pos[1], cur_pos[0]] = temp;
            nstate[cur_pos[1], cur_pos[0] + 1] = -1;
            ngval = node.gval + 1;
            nhval = set_hval(nstate);
            move_check.Add(new Node(nstate, ngval + nhval, ngval, nhval, node.cstate));
        }
        // direction[left]
        if (cur_pos[0] - 1 > -1)
        {
            nstate = (int[,])node.cstate.Clone();
            temp = nstate[cur_pos[1], cur_pos[0] - 1];
            nstate[cur_pos[1], cur_pos[0]] = temp;
            nstate[cur_pos[1], cur_pos[0] - 1] = -1;
            ngval = node.gval + 1;
            nhval = set_hval(nstate);
            move_check.Add(new Node(nstate, ngval + nhval, ngval, nhval, node.cstate));
        }
        // direction[up]
        if (cur_pos[1] - 1 > -1)
        {
            nstate = (int[,])node.cstate.Clone();
            temp = nstate[cur_pos[1] - 1, cur_pos[0]];
            nstate[cur_pos[1], cur_pos[0]] = temp;
            nstate[cur_pos[1] - 1, cur_pos[0]] = -1;
            ngval = node.gval + 1;
            nhval = set_hval(nstate);
            move_check.Add(new Node(nstate, ngval + nhval, ngval, nhval, node.cstate));
        }
        // direction[down]
        if (cur_pos[1] + 1 < 3)
        {
            nstate = (int[,])node.cstate.Clone();
            temp = nstate[cur_pos[1] + 1, cur_pos[0]];
            nstate[cur_pos[1], cur_pos[0]] = temp;
            nstate[cur_pos[1] + 1, cur_pos[0]] = -1;
            ngval = node.gval + 1;
            nhval = set_hval(nstate);
            move_check.Add(new Node(nstate, ngval + nhval, ngval, nhval, node.cstate));
        }

        return move_check;
    }

    // slove h(n)
    public int set_hval(int[,] cstate)
    {
        int val = 0;

        for (int i = 0; i < cstate.GetLength(0); i++)
        {
            for (int j = 0; j < cstate.GetLength(1); j++)
            {
                if (cstate[i, j] != this.gstate[i, j])
                {
                    val++;
                }
            }
        }

        return val;
    }


    // Specifically used to find the position of the blank space = -1
    public int[] find_blank_pos(int[,] state)
    {
        int[] blank = new int[2];
        for (int i = 0; i < state.GetLength(0); i++)
        {
            for (int j = 0; j < state.GetLength(1); j++)
            {
                if (state[i, j] == -1)
                {
                    blank[0] = j;
                    blank[1] = i;
                    return blank;     // return blank's (x,y) position
                }
            }
        }
        return blank;
    }

    

    // finally find goal_path
    public void final_path()
    {
        List<Node> tclose = this.close.ConvertAll(n => new Node(n.cstate, n.fval, n.gval, n.hval, n.parent)); //tempClose
        this.path = new List<int[,]>();
        Node cur_node;
        int[,] current;

        if (equal_check(tclose[tclose.Count - 1].cstate, this.gstate))
        {
            cur_node = tclose[tclose.Count - 1];
            this.path.Add(cur_node.cstate);
            current = cur_node.parent;
            while (!equal_check(this.rsstate, current))
            {
                foreach (Node n in tclose)
                {
                    if (equal_check(current, n.cstate))
                    {
                        this.path.Add(current);
                        current = n.parent;
                        tclose.Remove(n);
                        break;
                    }
                }
            }
            if (equal_check(this.rsstate, current))
            {
                this.path.Add(current);
            }
        }
        else
        {
            Debug.Log("Didn't arrive gstate");
        }
        this.path.Reverse();
    }

    public void set_initial()
    {
        int hval;

        this.open = new List<Node>();
        this.close = new List<Node>();
        hval = set_hval(this.rsstate);
        Node start = new Node(this.rsstate, 0 + hval, 0, hval, null);
        this.open.Add(start);
    }

    // print
    public void print_state(int[,] st)
    {
        string s = "\n  ";

        for (int i = 0; i < st.GetLength(0); i++)
        {
            for (int j = 0; j < st.GetLength(1); j++)
            {
                s += st[i, j] + " ";
            }
           
            s += "\n  ";
        }
        Debug.Log(s);
    }



    public void main()
    {
        set_initial();
        a_star_al();
        final_path();
    }

}