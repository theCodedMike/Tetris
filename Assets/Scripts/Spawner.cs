using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] blocks;
    
    [Header("方块自动下降频率，2表示每秒下降2次")]
    public float freq;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        int idx = Random.Range(0, blocks.Length);
        GameObject group = Instantiate(blocks[idx], transform.position, Quaternion.identity);
        group.GetComponent<Group>().SetFreq(freq);
    }
}
