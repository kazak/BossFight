using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;

    public BossHealth player;

    private void Start()
    {
        healthBar = GetComponent<Image>();  
        player = FindObjectOfType<BossHealth>();
    }
    private void Update()
    {
        healthBar.fillAmount = player.health/player.maxhealth;
    }


}
