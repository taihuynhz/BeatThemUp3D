using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] protected Image healthUI;
    [SerializeField] protected DamageReceiver damageReceiver;

    [System.Obsolete]
    protected void Reset()
    {
        healthUI = transform.FindChild("HPBar").GetComponent<Image>();
    }

    public void DisplayHP(float value)
    {
        value /= 100f;

        if (value <= 0) value = 0;
        healthUI.fillAmount = value;
    }

    public void DisplayEnemyHP(float value)
    {
        value /= 10f;

        if (value <= 0) value = 0;
        healthUI.fillAmount = value;
    }
}
