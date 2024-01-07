using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will apply continuous damage to the Player as long as it stay inside the trigger on the same object
/// </summary>
public class MyDamageZone : MonoBehaviour
{
    /// <summary>
    /// 碰撞检测停留的回调函数
    /// </summary>
    /// <param name="other"></param> <summary>
    /// 碰撞对象
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerContoller contoller = other.GetComponent<PlayerContoller>();

        if (contoller != null){
            contoller.ChangeHealth(-1);
        }
    }
}
