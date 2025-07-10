using UnityEngine;

public class Group : MonoBehaviour
{
    private float _freq;

    private float _time;

    private void Awake()
    {
        _freq = 0;
        _time = 0;
    }

    public void SetFreq(float freq)
    {
        _freq = freq;
    }
    
    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (_time * _freq >= 1)
        {
            _time = 0;
            transform.position += Vector3.down;
        }
        
        // 向左移动
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
        }
        // 向右移动
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
        }
        // 向下加速下落
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += Vector3.down;
        }
        // 旋转
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(0, 0, -90);
        }
    }
}
