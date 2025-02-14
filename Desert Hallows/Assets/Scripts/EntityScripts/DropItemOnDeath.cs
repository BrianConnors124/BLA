using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DropItemOnDeath : MonoBehaviour
{
    private Enemy parent;

    public ItemInfo[] itemToDrop;

    public GameObject objToDrop;

    [Range(0, 100)]public int[] rarity;

    public GameObject baseItem;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponent<Enemy>();
        parent.onDeath += DropItem;
        baseItem = GameObject.Find("_Base_Item");
    }

    private void DropItem()
    {
        for (var i = 0; i < itemToDrop.Length; i++)
        {
            var num = Random.Range(0, rarity[i]);
            
            if(num == 0)Instantiate(baseItem, transform.position, Quaternion.identity).GetComponent<ItemData>().UpdateInfo(itemToDrop[i]);
        }
        
    }

    public void DropHead()
    {
        Instantiate(objToDrop, transform.position - new Vector3(0, transform.localScale.y * .8f, 0), Quaternion.identity);
    }
}
