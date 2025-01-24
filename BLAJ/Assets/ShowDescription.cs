using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowDescription : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    public void UpdateDescription(ItemInfo item)
    {
        print("helloooo");
        itemImage.sprite = item.itemImage;
        itemText.text = item.itemName + "-\n\n" + item.description;
    }
}
