using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Item Data")] 
    
    [SerializeField] private ItemInfo currentItem;

    [SerializeField] private String previousItem;
    
    [SerializeField] private int slotQuantity;
    public bool isOccupied => !currentItem.itemName.Equals("");

    [Header("Item Slot")] 
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public InventoryManager manager;

    public EventSystem eventSystem;

    public bool isAbilitySlot;


    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
    
    private void UpdateVisualInfo()
    {
        if (slotQuantity == 0)
        {
            currentItem = manager.emptyItem;
            quantityText.enabled = false;
        }
        else
        {
            quantityText.text = slotQuantity.ToString();
            quantityText.enabled = true;
        }
        
        itemImage.sprite = currentItem.itemImage;
    }

    public void ChangeInfo(ItemInfo newItem, int newQuantity)
    {
        previousItem = currentItem.itemName;
        currentItem = newItem;
        slotQuantity = newQuantity;

        if (isAbilitySlot)
        {
            AAbilityFunction(previousItem).Invoke();
            AbilityFunction(currentItem).Invoke();
        }
        
        UpdateVisualInfo();
    }

    public int GetSlotQuantity()
    {
        return slotQuantity;
    }

    public void AddSlotQuantity(int change)
    {
        slotQuantity += change;
        UpdateVisualInfo();
    }

    public ItemInfo GetItem()
    {
        return currentItem;
    }
    
    public string GetItemName()
    {
        return currentItem.itemName;
    }


    public void UseItem()
    {
        ItemFunction(currentItem).Invoke();
    }
    
    private Action AAbilityFunction(string name)
    {
        Action commit = () => Debug.LogWarning("No action assigned to item, try checking the name! Remove Ability");
        
        if(name.ToUpper().Equals("DASH BOOK"))
        {
            commit = () => player.hasDash = false;
            

            return commit;
        }
        
        if (name.ToUpper().Equals("DASH ATTACK BOOK"))
        {
            commit = () => player.hasDashAttack = false;
            

            return commit;
        }
        
        if (name.ToUpper().Equals("SLAM ATTACK BOOK"))
        {
            commit = () => player.hasSlamAttack = false;
            

            return commit;
        } 

        return commit;
    }

    private Action AbilityFunction(ItemInfo item)
    {
        Action commit = () => Debug.LogWarning("No action assigned to item, try checking the name! Add Ability");
        if(item.itemName.ToUpper().Equals("DASH BOOK"))
        {
            commit = () => player.hasDash = true;
            

            return commit;
        }
        
        if (item.itemName.ToUpper().Equals("DASH ATTACK BOOK"))
        {
            commit = () => player.hasDashAttack = true;
            

            return commit;
        }
        
        if (item.itemName.ToUpper().Equals("SLAM ATTACK BOOK"))
        {
            commit = () => player.hasSlamAttack = true;
            

            return commit;
        }

        return commit;
    }

    private Action ItemFunction(ItemInfo item)
    {
        Action commit = () => Debug.LogWarning("No action assigned to item, try checking the name!");
        if (item.itemName.ToUpper().Equals("HEALTH POTION") && player.health != player.maxHealth)
        {
            
            commit = () => player.health += 35;
            if (player.health > player.maxHealth) commit += () => player.health = player.maxHealth;
            slotQuantity--;
            UpdateVisualInfo();
            
            return commit;
        }
        
        if (item.itemName.ToUpper().Equals("STRENGTH POTION"))
        {
            commit = () => player.damage += 4;
            slotQuantity--;
            UpdateVisualInfo();
            return commit;
        }

        return commit;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(gameObject); 
        manager.simpleUpdate.Invoke();
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Exited");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
