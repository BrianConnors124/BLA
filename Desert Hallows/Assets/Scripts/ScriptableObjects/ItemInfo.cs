using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Items", fileName = "New Item")]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemImage;

}
