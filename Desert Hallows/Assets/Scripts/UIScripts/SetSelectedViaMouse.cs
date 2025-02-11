using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedViaMouse : MonoBehaviour, IPointerEnterHandler
{
    public EventSystem eventSystem;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(gameObject);
    }
}
