using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemData> backpackSlot;
    
    [SerializeField] private int maxNumSlots;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var slot in backpackSlot)
        {
            slot.Attack();
        }
    }

    void PickUpItem(GameObject a)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
