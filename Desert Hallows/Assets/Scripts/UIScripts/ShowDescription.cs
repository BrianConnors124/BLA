using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ShowDescription : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    public EventSystem eventSystem;
    public InventoryManager manager;
    private GameObject oldSelected;

    private void Start()
    {
        manager.simpleUpdate += SimpleUpdate;
        InputSystemController.instance.updateDescription += () => oldSelected = eventSystem.currentSelectedGameObject;
        InputSystemController.instance.updateDescription += () => StartCoroutine(UpdateDescription());
        
    }

    private IEnumerator UpdateDescription()
    {
        yield return new WaitUntil(() => !oldSelected.name.Equals(eventSystem.currentSelectedGameObject.name));
        SimpleUpdate();
    }

    public void SimpleUpdate()
    {
        var item = eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().currentItem;
        itemImage.sprite = item.itemImage;
        itemText.text = item.itemName + "\n\n" + item.description;
    }
}
