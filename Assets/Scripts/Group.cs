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
            transform.position += Vector3.left;
            if(IsValidGridPos())
                UpdateGrid();
            else
                transform.position += Vector3.right;
        }
        // 按 向右箭头 向右移动
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            if(IsValidGridPos())
                UpdateGrid();
            else
                transform.position += Vector3.left;
        }
        // 按 向下箭头 加速下落
        if (Input.GetKeyDown(KeyCode.DownArrow) || _time * _freq >= 1f)
        {
            _time = 0;
            transform.position += Vector3.down;
            if(IsValidGridPos())
                UpdateGrid();
            else
            {
                transform.position += Vector3.up;
                
                // 删除所有填满的行
                Grid.DeleteFullRows();
                
                // 生成下一个方块，并禁用脚本
                _spawner.SpawnNext();
                enabled = false;
            }
        }
        // 按 空格键 旋转
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(0, 0, -90);
            if(IsValidGridPos())
                UpdateGrid();
            else
                transform.Rotate(0, 0, 90);
        }
    }

    // 更新网格状态
    private void UpdateGrid()
    {
        for (int y = 0; y < Grid.Height; y++)
        {
            for (int x = 0; x < Grid.Width; x++)
            {
                if (Grid.grid[x, y] != null)
                {
                    // 检测某一方块是否是该方块的一部分
                    if (Grid.grid[x, y].parent == transform)
                        // 移除旧地子方块
                        Grid.grid[x, y] = null;
                }
            }
        }

        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
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
            if (Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        
        return true;
    }
}
