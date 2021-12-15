using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Graph : MonoBehaviour
{

    Transform[] points;

    int[,] matrix;

    private void Awake() {
         matrix = new int[15, 15] {
            {-1, 0, -1, -1, -1, -1, -1, -1, -1, 0, -1, -1, -1, -1, -1},
            {0, -1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, 0, -1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, 0, -1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, 0, -1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, 0, -1, 0, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, 0, -1, 0, 0, -1, -1, -1, -1, 0, 0},
            {-1, -1, -1, -1, -1, -1, 0, -1, 0, -1, -1, -1, -1, -1, 0},
            {-1, -1, -1, -1, -1, -1, 0, 0, -1, -1, -1, -1, -1, -1, 0},
            {0, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, 0, -1, 0, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, -1, 0, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, -1, 0, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, -1, -1},
            {-1, -1, -1, -1, -1, -1, 100, 100, 100, -1, -1, -1, -1, -1, 100}
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        points = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        foreach(Transform point in points)
        {
            Gizmos.DrawSphere(point.position, 0.5f);
        }
        
    }

    public ArrayList GetPossibleDists(int cur_point)
    {
        ArrayList res = new ArrayList();
        for(int i = 0; i < 15; i++)
        {
            if(matrix[cur_point, i] == 0)
            {
                res.Add(i);
            } 
        }
        return res;
    }

    public Vector3 GetPointDirection(int point)
    {
        return transform.Find("" + point).position;
    }

    public int[,] GetMatrix()
    {
        return matrix;
    }

    public bool isStopped(int dist_point)
    {
        return Vector3.Distance(GameObject.FindGameObjectWithTag("Agent").transform.position, points[dist_point].position) < .1f;
    }
}
