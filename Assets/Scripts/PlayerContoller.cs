using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 无敌时间配置数据类型
/// </summary>
public struct InvincibleData
{

    /// <summary>
    /// 是否进入无敌状态
    /// </summary>
    public bool enable;

    /// <summary>
    /// 无敌时间倒计时
    /// </summary>
    public float timer;

    /// <summary>
    /// 无敌持续时间设置
    /// </summary>
    public float duration;

};

/// <summary>
/// 无敌时间服务类
/// </summary>
public class InvincibleService
{
    /// <summary>
    /// 无敌时间逻辑判断所需的数据
    /// </summary>
    public InvincibleData data;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="duration">无敌时间可以持续的时间</param>
    public InvincibleService(float duration)
    {
        // 默认关闭无敌模式
        data = new InvincibleData() { enable = false, timer = 0, duration = duration };
    }

    /// <summary>
    /// 无敌时间逻辑判断方法
    /// </summary>
    public void CheckInvincible()
    {
        if (data.enable)
        {
            data.timer += Time.deltaTime;
            if (data.timer >= data.duration)
            {
                EndInvincible();
            }
        }
    }

    /// <summary>
    /// 无敌时间服务类的开始方法
    /// </summary>
    public void StartInvincible()
    {
        data.enable = true;
    }

    /// <summary>
    /// 无敌时间服务类的结束方法
    /// </summary>
    public void EndInvincible()
    {
        data.enable = false;
        data.timer = 0;
    }


};

public class PlayerContoller : MonoBehaviour
{

    // 定义玩家移动速度
    public float speed = 5.0f;

    // 2d刚体组件句柄
    private Rigidbody2D _rigidbody2D;

    // 动画控制器句柄
    private Animator _animator;

    // 玩家朝向
    private Vector2 _lookDirection = Vector2.down;

    // 当前输入
    private Vector2 _currentInput;

    // 定义最大生命值
    public int max_health = 5;

    // 当前角色生命值
    private int _current_health;

    // 暴露_current_health的只读属性
    public int health => _current_health;

    // 无敌时间长度
    public float invincible_time = 2.0f;

    // 无敌时间配置类
    public InvincibleService invincible_service;

    // 重生点位置
    public Transform respawn_point;

    // Start is called before the first frame update
    void Start()
    {
        // 获取当前游戏对象上的Rigidbody2D组件，并将其赋值给_rigidbody2D变量
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // 获取动画控制器组件
        _animator = GetComponent<Animator>();

        // 复制当前生命值
        _current_health = max_health;

        // 配置无敌时间服务
        invincible_service = new InvincibleService(invincible_time);
    }

    // Update is called once per frame
    void Update()
    {
        // 更新当前输入
        _currentInput.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        // 更新动画控制参数
        if (!Mathf.Approximately(_currentInput.x, 0.0f) || !Mathf.Approximately(_currentInput.y, 0.0f))
        {
            _lookDirection.Set(_currentInput.x, _currentInput.y);
            _lookDirection.Normalize();
        }

        _animator.SetFloat("LookX", _lookDirection.x);
        _animator.SetFloat("LookY", _lookDirection.y);
        _animator.SetFloat("Speed", _currentInput.magnitude);


    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody2D.position;
        position += _currentInput * speed * Time.deltaTime;

        _rigidbody2D.MovePosition(position);

        // 计算无敌时间
        invincible_service.CheckInvincible();
    }

    /// <summary>
    /// 重生函数
    /// </summary>
    private void Respawn()
    {
        ChangeHealth(max_health);
        _rigidbody2D.position = respawn_point.position;
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (invincible_service.data.enable)
                return;
            invincible_service.StartInvincible();
        }

        _current_health = Mathf.Clamp(_current_health + amount, 0, max_health);
        string result = $"Player health changing:{amount}, Current health:{_current_health}";
        print(result);

        if (_current_health == 0)
        {
            Respawn();
        }
    }
}
