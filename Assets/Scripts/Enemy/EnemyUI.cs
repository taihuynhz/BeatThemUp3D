using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : HealthUI
{
    protected static EnemyUI instance;
    public static EnemyUI Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}

