using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        // 获取当前游戏对象上的Rigidbody2D组件，并将其赋值给_rigidbody2D变量
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // 获取动画控制器组件
        _animator = GetComponent<Animator>();
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
    }
}
