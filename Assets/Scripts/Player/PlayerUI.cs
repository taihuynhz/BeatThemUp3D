using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : HealthUI
{
    protected void Update()
    {
        UpdateHPBar();
    }

    protected void UpdateHPBar()
    {
        damageReceiver = GameObject.Find("PlayerChild").transform.GetComponent<PlayerDamageReceiver>();
        DisplayHP(damageReceiver.hP);
    }
}
