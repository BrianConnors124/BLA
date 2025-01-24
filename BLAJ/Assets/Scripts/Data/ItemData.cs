using System;
using UnityEngine; 


public class ItemData : MonoBehaviour
{
    public ItemInfo itemInfo;
    
    
    
    public GameObject image;
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
        image.GetComponent<SpriteRenderer>().sprite = itemInfo.itemImage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _inventoryManager.AddItem(itemInfo);
            Destroy(gameObject);
        }
    }
}

[CreateAssetMenu(menuName = "Items/New Items", fileName = "New Item")]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemImage;
    public int quantity;

}
