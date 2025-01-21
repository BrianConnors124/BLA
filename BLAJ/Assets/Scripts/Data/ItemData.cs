using System;
using UnityEngine; 


public class ItemData : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite image;
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _inventoryManager = GameObject.Find("PlayerInventory").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _inventoryManager.AddItem(itemName, quantity, image);
        }
    }
}
