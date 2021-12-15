using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Agent : MonoBehaviour {
    float[,] QMatrix;
    [SerializeField] float speed = .5f;
    [SerializeField] Graph path;
    [SerializeField] bool learning = true;
    System.Random rnd;
    int prev_point = 0, point = 0;
    private void Start() {
        QMatrix = new float[15, 15] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };
        rnd = new System.Random();

        point = GetNextPoint();
    }

    private void Update() {

        if(path.isStopped(point))
        {
            FindNextPoint();
        }
        else
        {
            transform.LookAt(path.GetPointDirection(point));
            transform.position = Vector3.MoveTowards(transform.position, path.GetPointDirection(point), speed * Time.deltaTime);
        }
    }

    private void FindNextPoint()
    {
        if(!learning)
        {
            point = FindQMaxIDX(point);
            print("Learning is finished: ");
        }
        else if (point != 14)
        {
            int next_point = FindQMaxIDX(point) > 0 ? FindQMaxIDX(point) : GetNextPoint();
            QMatrix[prev_point, point] = path.GetMatrix()[prev_point, point] + .8f * FindQMax(next_point);
            print("Reached point: " + point);
            prev_point = point;
            point = next_point;
        }
        else
        {
            QMatrix[prev_point, point] = path.GetMatrix()[prev_point, point] + .8f * 100;
            printArray();
            point = rnd.Next(0, 13);
        }
    }

    private int GetNextPoint()
    {
        ArrayList ways = path.GetPossibleDists(point);
        int idx = rnd.Next(0, ways.Count);
        print("Index is: " + idx);
        print("Chosen next point: " + ways[idx]);
        return (int) ways[idx];
    }

    private int FindQMaxIDX(int point)
    {
        float max = float.MinValue;
        int idx = 0;
        for(int i = 0; i < 15; i++)
        {
            if(max < QMatrix[point, i])
            {
                max = QMatrix[point, i];
                idx = i;
            }
        }
        return idx;
    }

    private float FindQMax(int point)
    {
        float max = float.MinValue;
        for(int i = 0; i < 15; i++)
        {
            if(max < QMatrix[point, i])
                max = QMatrix[point, i];
        }
        return max;
    }

    private void printArray()
    {
        var result = string.Empty;
        var maxI = 15;
        var maxJ = 15;
        for (var i = 0; i < maxI; i++)
        {
            result += ",{";
            for (var j = 0; j < maxJ; j++)
            {
                result += $"{QMatrix[i, j]},";
                if(QMatrix[i,j] > 0)
                {
                    print("FINALLY! RES: " + QMatrix[i, j]);
                }
            }

            result += "}";
        }

        result = result.Replace(",}", "}").Substring(1);
        print(result);
    }
}