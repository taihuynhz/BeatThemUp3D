using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyDamageSender : DamageSender
{
    protected override void InitAnimationEvent()
    {
        SetAnimationEvent(0.3f, 0);
        SetAnimationEvent(0.3f, 1);
        SetAnimationEvent(0.3f, 2);
    }
}
