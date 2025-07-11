using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Done_Spawner : MonoBehaviour {
    public GameObject[] Blocks;//储存方块组的数组
    public Sprite[] sprites;//储存方块组图片的数组，用于界面提示下一个产生的方块组
    public static bool isFirst = true;//是否第一次产生方块
    public static int current = 0;  //当前方块的序号
    public static int next = 0;     //下一个产生的方块序号

	// Use this for initialization
	void Start () {
        SpawnerNext();
    }
    
    /// <summary>
    /// 产生方块组
    /// </summary>
    public void SpawnerNext()
    {
       
        if (isFirst)
        {
            isFirst = false;
            current = Random.Range(0, Blocks.Length);
            next = Random.Range(0, Blocks.Length);
        }
        else
        { 
            current = next;
            next = Random.Range(0, Blocks.Length);
        }

        //随机产生方块
        Instantiate(Blocks[current], transform.position, Quaternion.identity);
        //在界面中显示出图片
        GameObject.Find("Image").GetComponent<Image>().sprite = sprites[next];

    }

}
