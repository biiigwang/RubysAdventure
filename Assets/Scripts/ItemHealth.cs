using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : MonoBehaviour
{
    // 碰撞检测触发事件
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 当草莓🍓与别的物体发生碰撞时触发，尝试获取PlayerController变量
        PlayerContoller contoller = other.GetComponent<PlayerContoller>();

        if (contoller)
        {
            // 调用玩家改变血量的API增加1点HP
            contoller.ChangeHealth(1);
            //  销毁自身
            Destroy(gameObject);
        }
    }
}
