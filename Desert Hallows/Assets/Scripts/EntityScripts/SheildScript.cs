using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildScript : MonoBehaviour
{
    public float hitPoints;
    private GameObject controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public virtual void ReceiveDamage(float damage)
    {
        hitPoints -= damage;
        ObjectPuller.PullObjectAndSetTextAndColor(controller.GetComponent<ObjectLists>().damageNumbers, transform.position, "BLocked", Color.cyan);
        if(hitPoints <= 0)Destroy(gameObject);
    }
}
