using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_Group : MonoBehaviour {

    public float lastFall = 0;

	// Use this for initialization
	void Start () {
        if (!isValidGridPos())
        {
            Debug.Log("Game Over");  
            Destroy(gameObject);
        }
    }
	
    /// <summary>
    /// 检测用户输入，移动方块
    /// </summary>
 
	// Update is called once per frame
	void Update () {

        //向左
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (isValidGridPos())
            {
                updateGrid();

            }
            else
                transform.position += new Vector3(1, 0, 0);
        }

        //向右
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (isValidGridPos())
            {
                updateGrid();

            }
            else
                transform.position += new Vector3(-1, 0, 0);
        }

        //旋转
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            if (isValidGridPos())
            {
                updateGrid();

            }
            else
                transform.Rotate(0,0,90);
        }

        //加速下落
        if (Input.GetKeyDown(KeyCode.DownArrow)||Time.time-lastFall>1)
        {
            transform.position += new Vector3(0, -1, 0);

            if (isValidGridPos())
            {
                updateGrid();

            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                Done_Grid.DeleteFullRows();

                //FindObjectsOfType<Spawner>().spawnNext();
                FindObjectOfType<Done_Spawner>().SpawnerNext();

                enabled = false;

            }

            lastFall = Time.time;
                
        }

    }

    /// <summary>
    /// 位置是否合理
    /// </summary>
    /// <returns></returns>
    bool isValidGridPos()
    {
        foreach(Transform child in transform)
        {
            Vector2 v = Done_Grid.RoundVec2(child.position);

            if(!Done_Grid.InsideBorder(v))
            {
                return false;
            }
            if(Done_Grid.grid[(int)v.x,(int)v.y]!=null&&Done_Grid.grid[(int)v.x,(int)v.y].parent!=transform)
            {
                return false;
            }
        }

        return true;
        
    }

    /// <summary>
    /// 更新网格状态
    /// </summary>
    void updateGrid()
    {
        for(int y = 0; y < Done_Grid.height; y++)
        {
            for(int x = 0; x < Done_Grid.width; x++)
            {
                if(Done_Grid.grid[x,y]!=null)
                {
                    if(Done_Grid.grid[x,y].parent==transform)
                    {
                        Done_Grid.grid[x, y] = null;
                    }
                }
            }
        }

        foreach(Transform child in transform)
        {
            Vector2 v = Done_Grid.RoundVec2(child.position);
            Done_Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
