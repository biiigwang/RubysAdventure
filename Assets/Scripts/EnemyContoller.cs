using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingDirection
{
    horizontal,
    vertical
}

public class EnemyContoller : MonoBehaviour
{



    /// <summary>
    /// 移动速度
    /// </summary>
    public float speed = 2.0f;

    /// <summary>
    /// 切换移动方向的时间
    /// </summary>
    public float timeToChangeDirection = 5.0f;

    /// <summary>
    /// 切换移动方向计时器
    /// </summary>
    private float _changeDirectionTimer = 0.0f;

    /// <summary>
    /// 角色移动方向配置
    /// </summary>
    public MovingDirection movingDirection = MovingDirection.horizontal;

    /// <summary>
    /// 角色移动方向
    /// </summary>
    private Vector2 _direction = Vector2.right;


    /// <summary>
    /// 私有变量 _rigidbody2D 用于存储 Rigidbody2D 对象
    /// </summary>
    private Rigidbody2D _rigidbody2D;


    /// <summary>
    /// 获取Animator中lookX属性的整数类型索引值
    /// </summary>
    /// <returns>lookX属性的整数类型索引值</returns>
    private static readonly int _lookX = Animator.StringToHash("lookX");

    /// <summary>
    /// 获取Animator中lookX属性的整数类型索引值
    /// </summary>
    /// <returns>lookX属性的整数类型索引值</returns>
    private static readonly int _lookY = Animator.StringToHash("lookY");

    /// <summary>
    /// 动画控制器句柄
    /// </summary>
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        // 初始化刚体
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // 初始化计时器
        _changeDirectionTimer = timeToChangeDirection;

        // 初始化动画控制器
        _animator = GetComponent<Animator>();

        // 根据配置初始化运动方向
        _direction = movingDirection == MovingDirection.horizontal ? Vector2.left : Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        // 根据时间切换移动方向
        _changeDirectionTimer -= Time.deltaTime;
        if (_changeDirectionTimer <= 0.0f)
        {
            _changeDirectionTimer = timeToChangeDirection;
            _direction *= -1;
            // print($"({_direction.x}, {_direction.y})");
        }

        // 设置动画控制器参数
        _animator.SetFloat(_lookX, _direction.x);
        _animator.SetFloat(_lookY, _direction.y);

    }

    private void FixedUpdate()
    {
        // 移动角色
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direction * speed * Time.deltaTime);
    }

    /// <summary>
    /// 碰撞回调函数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerContoller contoller = other.collider.GetComponent<PlayerContoller>();

        if (contoller != null)
        {
            contoller.ChangeHealth(-1);
        }
    }
}
