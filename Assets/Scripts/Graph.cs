using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField, Range(10, 100)] private int resolution = 10;
    [SerializeField] FunctionLibrary.FunctionName function;

    private Transform[] points;
    // Start is called before the first frame update
    // resolution就是有多少的点。

    private void Awake()
    {
        points = new Transform[resolution * resolution];
        float step = 2f / resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;
        // 因为我们的目的是把points限制在x轴-1 ~ 1范围内，所以是2/resolution。本文中，
        // 用*step代替之前是/5f。迭代是10次
        //使用step的主要意义是自动缩放，自动把点以合适的大小塞进2格大小里。

        // 下面就是i++的正确用法。因为如果++i会导致少一次迭代，而i++先返回值再自增符合要求
        //注意！for循环里，一旦判定i<10，就会将当前的i值返回，带到下面代码里运算，运算完i才会++。
        //所以第一次迭代的时候，i==0而不是1.


        //下面第一行原来这么写的：for (int i = 0; i < resolution; i++),
        //其实和现在没啥区别，因为points array的长度就是resolution
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;//Each time we finish a row we have to reset x to zero.
                z += 1;
                //This variable must not be incremented each iteration.
                //Instead, it only increments when we move on to the next row
            }
            Transform point = Instantiate(pointPrefab);
            points[i] = point;
            position.x = (x + 0.5f) * step - 1f;
            position.z = (z + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            //position.y = position.x * position.x * position.x;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            //if (function == 0)
            //{
            //    position.y = FunctionLibrary.Wave(position.x, time);
            //}
            //else if (function == 1)
            //{
            //    position.y = FunctionLibrary.MultiWave(position.x, time);
            //}
            //else if (function == 2)
            //{
            //    position.y = FunctionLibrary.Ripple(position.x, time);
            //}

            position.y = f(position.x, position.z, time);
            point.localPosition = position;
            //position.y = Mathf.Sin(Mathf.PI * position.x);
            // scale the time by π as well the function will repeat every two seconds.
        }
    }
}