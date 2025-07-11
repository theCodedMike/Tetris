using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] blocks;
    public Sprite[] sprites;
    [Header("方块自动下降频率，2表示每秒下降2次")]
    public float freq;
    
    private static bool _isFirst = true;
    private static int _curr; // 当前方块的序号
    private static int _next; // 下一个产生的方块序号

    private Image _nextGroup;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _nextGroup = GameObject.Find("Image").GetComponent<Image>();
        
        SpawnNext();
    }

    public void SpawnNext()
    {
        if (_isFirst)
        {
            _isFirst = false;
            _curr = Random.Range(0, blocks.Length);
        }
        else
            _curr = _next;
        _next = Random.Range(0, blocks.Length);
        
        GameObject group = Instantiate(blocks[_curr], transform.position, Quaternion.identity);
        group.GetComponent<Group>().SetFreq(freq);
        
        _nextGroup.sprite = sprites[_next];
    }
}
