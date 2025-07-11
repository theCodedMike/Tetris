using System;
using UnityEngine;

public class Group : MonoBehaviour
{
    private float _freq; // 自动下降频率，2表示每秒下降2次
    private float _time;
    private Spawner _spawner;
    

    private void Start()
    {
        _spawner = FindFirstObjectByType<Spawner>();
        
        if (!IsValidGridPos())
        {
            print("Game Over!");
            Destroy(gameObject);
        }
    }

    public void SetFreq(float freq)
    {
        _freq = freq;
    }
    
    // Update is called once per frame
    private void Update()
    {
        _time += Time.deltaTime;
        
        // 按 向左箭头 向左移动
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Operate(
                () => transform.position += Vector3.left, 
                () => transform.position += Vector3.right
                );
        }
        // 按 向右箭头 向右移动
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Operate(
                () => transform.position += Vector3.right, 
                () => transform.position += Vector3.left
                );
        }
        // 按 向下箭头 加速下落
        if (Input.GetKeyDown(KeyCode.DownArrow) || _time * _freq >= 1f)
        {
            Operate(() =>
            {
                _time = 0;
                transform.position += Vector3.down;
            }, () =>
            {
                transform.position += Vector3.up;
                
                // 删除所有填满的行
                Grid.DeleteFullRows();
                
                // 生成下一个方块，并禁用脚本
                _spawner.SpawnNext();
                enabled = false;
            });
        }
        // 按 空格键 旋转
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Operate(
                () => transform.Rotate(0, 0, -90), 
                () => transform.Rotate(0, 0, 90)
                );
        }
    }

    private void Operate(Action operate, Action actIfInvalid)
    {
        operate();
        if (IsValidGridPos())
            UpdateGrid();
        else
            actIfInvalid();
    }
    
    // 更新网格状态
    private void UpdateGrid()
    {
        for (int y = 0; y < Grid.Height; y++)
        {
            for (int x = 0; x < Grid.Width; x++)
            {
                if (Grid.grid[y, x] != null)
                {
                    // 检测某一方块是否是该方块的一部分
                    if (Grid.grid[y, x].parent == transform)
                        // 移除旧地子方块
                        Grid.grid[y, x] = null;
                }
            }
        }

        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            Grid.grid[(int)v.y, (int)v.x] = child;
        }
    }

    // 位置是否合法
    private bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            
            // 如果不在边界内
            if (!Grid.InsideBorder(v))
                return false;

            // 如果在边界内，但是所在位置有其他方块组
            if (Grid.grid[(int)v.y, (int)v.x] != null && Grid.grid[(int)v.y, (int)v.x].parent != transform)
                return false;
        }
        
        return true;
    }
}
