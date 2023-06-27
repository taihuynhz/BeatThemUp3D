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

        SetShootingEvent(0.1f, 6);
        SetThrowKnifeEvent(0.2f, 7);
    }

    protected void SetShootingEvent(float time, int attackClipIndex)
    {
        AnimationEvent shootingEvent = new AnimationEvent();
        shootingEvent.time = time;
        shootingEvent.functionName = "Shooting";
        attackClip[attackClipIndex].AddEvent(shootingEvent);
    }

    protected void SetThrowKnifeEvent(float time, int attackClipIndex)
    {
        AnimationEvent throwKnifeEvent = new AnimationEvent();
        throwKnifeEvent.time = time;
        throwKnifeEvent.functionName = "ThrowKnife";
        attackClip[attackClipIndex].AddEvent(throwKnifeEvent);
    }

    protected void Shooting()
    {
        MuzzleFlashFXSpawner.Instance.SpawnMuzzleFlashFX();
        BulletSpawner.Instance.SpawnBullet();
    }

    protected void ThrowKnife()
    {
        KnifeSpawner.Instance.SpawnKnife();
    }
}
