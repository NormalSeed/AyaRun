using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isControllActivated { get; set; } = true;

    private PlayerStatus _status;
    private PlayerMovement _movement;

    private void Awake() => Init();
    private void Update() => HandlePlayerControll();

    private void Init()
    {
        _status = GetComponent<PlayerStatus>();
        _movement = GetComponent<PlayerMovement>();
        // Test
        _status.CurrentHp.Value = _status.MaxHp;
        Debug.Log($"플레이어 체력: {_status.CurrentHp.Value}");

    }

    private void HandlePlayerControll()
    {
        if (!isControllActivated) return;

        HandleMovement();
        // Test
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Recover(1);
        }
    }

    private void HandleMovement()
    {
        float moveSpeed = _status.MoveSpeed;
        Vector3 moveDir = _movement.MoveHorizontal(moveSpeed);
        _status.IsMoving.Value = (moveDir != Vector3.zero);
        _movement.Jump(_status.JumpPower);
    }

    private void HandleSkill()
    {

    }

    public void TakeDamage(int damage)
    {
        _status.CurrentHp.Value -= damage;
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
}
