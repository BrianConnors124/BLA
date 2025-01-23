using System;
using UnityEngine; 


public class ItemData : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    private Sprite itemImage;
    public GameObject image;
    private InventoryManager _inventoryManager;

    private void Start()
    {
        itemImage = image.GetComponent<SpriteRenderer>().sprite;
        _inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _inventoryManager.AddItem(itemName, quantity, itemImage);
            Destroy(gameObject);
        }
    }
}
