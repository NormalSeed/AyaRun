using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class PlayerStatus : MonoBehaviour
{
    [field: SerializeField]
    [field: Range(0, 10)]
    public float MoveSpeed { get; set; } = 5;
    [field: SerializeField]
    [field: Range(0, 10)]
    public float JumpPower { get; set; }

    [field: SerializeField]
    [field: Range(5, 10)]
    public int MaxHp { get; set; }

    // Player Status---
    public ObservableProperty<int> CurrentHp { get; private set; } = new();

    // Player Action---
    public ObservableProperty<bool> IsMoving { get; private set; } = new();
    public ObservableProperty<bool> IsJumping { get; private set; } = new();
    public ObservableProperty<bool> IsAttacking { get; private set; } = new();
}
