using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public const int Width = 10; // 游戏场景的宽度
    public const int Height = 20; // 游戏场景的高度
    public static readonly Transform[,] grid = new Transform[Height, Width];

    private static int _score; // 分数
    
    
    // 对坐标进行取整
    public static Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // 方块组是否在边界内
    public static bool InsideBorder(Vector2 pos)
    {
        return (int)pos.x is >= 0 and < Width && (int)pos.y >= 0;
    }

    // 判断一行是否被填满
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            if (grid[y, x] == null)
                return false;
        }

        return true;
    }

    // 删除一行
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            Destroy(grid[y, x].gameObject);
            grid[y, x] = null;
        }
    }

    // 删除一行后，将上面的一行下移
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            if (grid[y, x] != null)
            {
                grid[y - 1, x] = grid[y, x];
                grid[y, x] = null;
                grid[y - 1, x].position += Vector3.down;
            }
        }
    }

    // 将上面所有行往下移
    public static void DecreaseRowAbove(int y)
    {
        for(int i = y; i < Height; i++) 
            DecreaseRow(i);
    }

    // 删除所有被填满的行
    public static void DeleteFullRows()
    {
        for (int y = 0; y < Height; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                _score++;
                UpdateScore();
                DecreaseRowAbove(y + 1);
                y--;
            }
        }
    }

    // 设置分数
    public static void UpdateScore()
    {
        GameObject.Find("Score").GetComponent<Text>().text = $"{_score}";
    }
}
