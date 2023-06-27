using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject panel;
    protected void Update()
    {
        if (player.GetComponentInChildren<PlayerDamageReceiver>().isDead)
        {
            StartCoroutine(ONGameOverPanel());
        }
    }

    protected IEnumerator ONGameOverPanel()
    {
        yield return new WaitForSeconds(2);
        panel.SetActive(true);
    }
}

