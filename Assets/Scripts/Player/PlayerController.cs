using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isControllActivated { get; set; } = true;
    private bool isGrounded;

    private PlayerStatus _status;
    private PlayerMovement _movement;
    private Animator _animator;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEvents();
    private void Update() => HandlePlayerControll();
    private void OnDisable() => UnsubscribeEvents();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Init()
    {
        _status = GetComponent<PlayerStatus>();
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void HandlePlayerControll()
    {
        if (!isControllActivated) return;

        HandleMovement();
        HandleSkill();
    }

    private void HandleMovement()
    {
        float moveSpeed = _status.MoveSpeed;
        Vector3 moveDir = _movement.MoveHorizontal(moveSpeed);
        _status.IsRunning.Value = (new Vector3(moveDir.x, 0, 0) != Vector3.zero);
        _movement.Rotate();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _movement.Jump(_status.JumpPower);
            _status.IsJumping.Value = true;
            isGrounded = false;
        }
        else
        {
            _status.IsJumping.Value = false;
        }
    }

    private void HandleSkill()
    {
        if (Input.GetKeyDown(KeyCode.Q) && SkillManager.Instance._qSkill.CanUseSkill())
        {
            SkillManager.Instance._qSkill.CanUseSkill();
            _status.IsUsingQ.Value = true;
        }
        else _status.IsUsingQ.Value = false;
    }

    public void TakeDamage(int damage)
    {
        if (_status.CurrentHp.Value >0)
        {
            _status.CurrentHp.Value -= damage;
        }
        
        Debug.Log($"플레이어 체력: {_status.CurrentHp.Value}");
        if (_status.CurrentHp.Value <= 0) Dead();
    }

    public void Recover(int healAmount)
    {
        if (_status.CurrentHp.Value >= _status.MaxHp) return;
        else _status.CurrentHp.Value += healAmount;
        Debug.Log($"플레이어 체력: {_status.CurrentHp.Value}");
    }

    public void Dead()
    {
        Debug.Log("플레이어 사망");
    }

    public void SubscribeEvents()
    {
        _status.IsUsingQ.Subscribe(SetQSkillAnimation);
        _status.IsRunning.Subscribe(SetRunAnimation);
        _status.IsJumping.Subscribe(SetJumpAnimation);
    }

    public void UnsubscribeEvents()
    {
        _status.IsUsingQ.Unsubscribe(SetQSkillAnimation);
        _status.IsRunning.Unsubscribe(SetRunAnimation);
        _status.IsJumping.Unsubscribe(SetJumpAnimation);
    }

    private void SetQSkillAnimation(bool value) => _animator.SetBool("IsUseQ", value);
    private void SetRunAnimation(bool value) => _animator.SetBool("IsRun", value);
    private void SetJumpAnimation(bool value) => _animator.SetBool("IsJump", value);
}
