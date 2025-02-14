using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{

    public float HP;
    private GameObject controller;
    public Action onTakeDamage;
    public GameObject gameOverScreen;

    public void Awake()
    {
        
    }

    public void Start()
    {
        controller = GameObject.Find("Main Camera");
    }


    public void ReceiveDamage(float damage)
    {
        HP -= damage;
        onTakeDamage.Invoke();
        ObjectPuller.PullObjectAndSetTextAndColor(controller.GetComponent<ObjectLists>().damageNumbers, transform.position, "" + (int) damage, Color.red);
        if (HP <= 0) Die();
    }

    private void Die()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }
    
    
}
