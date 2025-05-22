using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public QSkill _qSkill;
    private void Awake() => Init();
    private void Init()
    {
        base.SingletonInit();
        _qSkill = GetComponent<QSkill>();
    }
}
