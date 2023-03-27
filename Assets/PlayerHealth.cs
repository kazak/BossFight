using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead = false;
    public Slider slider;
    public Text text;
    [Header ("Damage Screen")]
    public Color damageColor;
    public Image damageImage;
    float colorSmoothing =6f;
    bool isTakingDamage = false;
    public AudioSource damageAudioSource;
   
    private void Awake()
    {

        singleton = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        slider.value = maxHealth;
        UpdateHealthCounter();
        
    }

    
    void Update()
    {   if(isTakingDamage)
        {
            damageImage.color = damageColor;
            damageAudioSource.Play();
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);
        }
        isTakingDamage = false;

        
    }
    public void PlayerDamage(float damage)
    {
        if (damage >= currentHealth) 
        {
            isTakingDamage = false;
            Dead();
            
        }
        else
        {   isTakingDamage = true;
            currentHealth -= damage;
            slider.value -= damage;
        }
        UpdateHealthCounter();
    }
    void UpdateHealthCounter()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        text.text = $"{currentHealth}/{maxHealth}";
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }
    
    void Dead()
    {
        currentHealth = 0;
        isDead = true;

    }
}
