using UnityEngine;

public class Grid : MonoBehaviour
{
    public const int Width = 10; // 游戏场景的宽度
    public const int Height = 20; // 游戏场景的高度
    public static readonly Transform[,] grid = new Transform[Width, Height];
    
    
    
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
    
}
