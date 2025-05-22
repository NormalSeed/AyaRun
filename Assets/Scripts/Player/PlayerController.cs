using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isControllActivated { get; set; } = true;

    private PlayerStatus _status;
    private PlayerMovement _movement;

    [SerializeField] private Animator _animator;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEvents();
    private void Update() => HandlePlayerControll();
    private void OnDisable() => UnsubscribeEvents();

    private void Init()
    {
        _status = GetComponent<PlayerStatus>();
        _movement = GetComponent<PlayerMovement>();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkillManager.Instance._qSkill.CanUseSkill();
        }
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

    private void Dead()
    {
        Debug.Log("플레이어 사망");
    }

    public void SubscribeEvents()
    {
        _status.IsRunning.Subscribe(SetRunAnimation);
    }

    public void UnsubscribeEvents()
    {
        _status.IsRunning.Unsubscribe(SetRunAnimation);
    }

    private void SetRunAnimation(bool value) => _animator.SetBool("IsRun", value);
}
