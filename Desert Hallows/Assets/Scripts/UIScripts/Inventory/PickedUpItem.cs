using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public class PickedUpItem : MonoBehaviour
{
    private List<ItemInfo> previousItems;
    public List<GameObject> items;

    public ItemInfo emptyItem;
    private UniversalTimer timer;

    private void Start()
    {
        previousItems = new List<ItemInfo>();
        timer = GetComponent<UniversalTimer>();
    }

    private void AddItem(ItemInfo item, int quantity)
    {
        for (var i = 0; i < items.Count - 1; i++)
        {
            var sprite = items[i + 1].GetComponent<Image>().sprite;
            ChangeDisplay(i, sprite, items[i + 1].GetComponentInChildren<TextMeshProUGUI>().text);   
        }
        
        ChangeFinalDisplay(item.itemImage, quantity);
    }

    private void ChangeDisplay(int i, Sprite sprite, string text)
    {
        items[i].GetComponent<Image>().sprite = sprite;
        items[i].GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
    
    private void ChangeFinalDisplay(Sprite sprite, int i)
    {
        items[^1].GetComponent<Image>().sprite = sprite;
        items[^1].GetComponentInChildren<TextMeshProUGUI>().text = "+" + i;
    }

    private void RemoveLastItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].GetComponent<Image>().sprite.name.Equals(emptyItem.itemImage.name))
            {
                items[i].GetComponent<Image>().sprite = emptyItem.itemImage;
                items[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                if (i != items.Count - 1)
                {
                    timer.SetActionTimer("Remove New Item" + i, 1, RemoveLastItem);
                }
                return;
            }
        }
    }

    public void PickedUpNewItem(ItemInfo item, int quantity)
    {
        AddItem(item, quantity);
        timer.SetActionTimer("Remove New Item", 1, RemoveLastItem);
    }
    
    
}
