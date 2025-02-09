using System;
using UnityEngine; 


public class ItemData : MonoBehaviour
{
    public ItemInfo itemInfo;
    public int quantity;
    public GameObject image;
    private InventoryManager _inventoryManager;
    private SpriteRenderer sprite; 

    private void Awake()
    {
        _inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
        sprite = image.GetComponent<SpriteRenderer>();
        UpdateInfo(itemInfo);
    }

    public void UpdateInfo(ItemInfo item)
    {
        itemInfo = item;
        quantity = 1;
        sprite.sprite = item.itemImage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _inventoryManager.AddItem(itemInfo, quantity);
            Destroy(gameObject);
        }
    }
}
