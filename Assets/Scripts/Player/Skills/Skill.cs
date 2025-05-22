using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float _cooldown;
    protected float _cooldownTimer;

    protected virtual void Update()
    {
        if (_cooldownTimer > 0) _cooldownTimer -= Time.deltaTime;
        else if (_cooldownTimer < 0) _cooldownTimer = 0;
    }

    public virtual bool CanUseSkill()
    {
        if (_cooldownTimer <= 0)
        {
            UseSkill();
            _cooldownTimer = _cooldown;
            return true;
        }
        Debug.Log("Skill is cooldown");
        return false;
    }

    public virtual void UseSkill()
    {

    }
}
