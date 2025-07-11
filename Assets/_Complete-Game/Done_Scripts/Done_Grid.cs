﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 储存一些函数
/// </summary>

public class Done_Grid : MonoBehaviour {

    public static int width = 10;//游戏场景的宽度
    public static int height = 20;//游戏场景的高度
    public static int score = 0;//分数
    


    public static Transform[,] grid = new Transform[width, height];

    /// <summary>
    /// 对坐标进行取整
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    /// <summary>
    /// 放快组是否在边界内
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    /// <summary>
    /// 删除行
    /// </summary>
    /// <param name="y">行号</param>
    public static void DeleteRow(int y)
    {
        for(int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    /// <summary>
    /// 将删除行的上面一行下降
    /// </summary>
    /// <param name="y">行号</param>

    public static void DecreaseRow(int y)
    {
        for(int x = 0; x < width; x++)
        {
            if(grid[x,y]!=null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;               //将上一行的方块一下去

                grid[x, y - 1].position += new Vector3(0, -1, 0);//坐标下落
            }
        }
    }

    /// <summary>
    /// 将上面所有行往下移
    /// </summary>
    /// <param name="y">行号</param>
    public static void DecreaseRowAbove(int y)
    {
        for(int i = y;i<height;i++)
        {
            DecreaseRow(i);
        }
    }

    /// <summary>
    /// 判断一行是否被填满
    /// </summary>
    /// <param name="y">行号</param>
    /// <returns></returns>
    public static bool IsRowFull(int y)
    {
        for(int x = 0; x < width; x++)
        {
            if(grid[x,y]==null)
            {
                
                return false;    
            }
        }
        return true;
    }

       /// <summary>
       /// 删除所有填满的行
       /// 1、先判断一行是否填满，若填满，就删除
       /// 2、删除上面填满的行。
       /// 3、分数+1
       /// </summary>
    public static void DeleteFullRows()
    {
        for(int y = 0; y < height; y++)
        {
            if(IsRowFull(y))
            {
                DeleteRow(y);
                score++;
                SetScore(score);
                DecreaseRowAbove(y + 1);
                y--;
            }
        }
    }

    /// <summary>
    /// 设置分数
    /// </summary>
    /// <param name="s">分数</param>
    public static void SetScore(int s)
    {
        GameObject.Find("Score").GetComponent<Text>().text = ""+s;
    }


}
