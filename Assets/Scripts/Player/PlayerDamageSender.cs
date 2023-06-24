using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSender : DamageSender
{
    protected override void InitAnimationEvent()
    {
        SetAnimationEvent(0.1f, 0);
        SetAnimationEvent(0.04f / 0.3f, 1);
        SetAnimationEvent(0.1f, 2);
        SetAnimationEvent(0.1f, 3);
        SetAnimationEvent(0.2f, 4);
        SetAnimationEvent(0.2f, 5);

        SetFireGunEvent(0.1f, 6);
    }

    protected void SetFireGunEvent(float time, int attackClipIndex)
    {
        AnimationEvent sendDamageEvent = new AnimationEvent();
        sendDamageEvent.time = time;
        sendDamageEvent.functionName = "Shooting";
        attackClip[attackClipIndex].AddEvent(sendDamageEvent);
    }

    protected void Shooting()
    {
        MuzzleFlashFXSpawner.Instance.SpawnMuzzleFlashFX();
        BulletSpawner.Instance.SpawnBullet();
    }
}
