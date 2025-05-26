using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSkill : Skill
{
    [SerializeField] private int _skillDamage;
    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log($"W스킬 실행\n{_skillDamage}만큼의 데미지를 준다.");
    }
}
