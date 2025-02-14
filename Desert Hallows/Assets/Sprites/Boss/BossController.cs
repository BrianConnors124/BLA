using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{

    public float HP;


    public void ReceiveDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0) Die();
    }

    private void Die()
    {
        print("GameOver");
        SceneManager.GetSceneByName("MainScene");
    }
    
    
}
