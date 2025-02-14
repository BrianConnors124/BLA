using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    
    public Image healthShadeSlider;
    public float maxHealth;
    private BossController controller;
    

    
    void Start()
    {
        controller =  GetComponent<BossController>();
        controller.onTakeDamage += UpdateHealthSlider;
        maxHealth = controller.HP;
    }
    
    
    private void UpdateHealthSlider()
    {
        healthShadeSlider.fillAmount = (maxHealth - controller.HP) / maxHealth;
    }
}
