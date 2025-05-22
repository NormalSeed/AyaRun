using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isControllActivated { get; set; } = true;

    private PlayerStatus _status;
    private PlayerMovement _movement;
    private Animator _animator;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEvents();
    private void Update() => HandlePlayerControll();
    private void OnDisable() => UnsubscribeEvents();

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
        _status.IsRunning.Value = (moveDir != Vector3.zero);
        _movement.Jump(_status.JumpPower);
        _animator.SetFloat("Speed", moveDir.magnitude);
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
    }

    public void UnsubscribeEvents()
    {
        _status.IsUsingQ.Unsubscribe(SetQSkillAnimation);
    }

    private void SetQSkillAnimation(bool value) => _animator.SetBool("IsUseQ", value);
}
