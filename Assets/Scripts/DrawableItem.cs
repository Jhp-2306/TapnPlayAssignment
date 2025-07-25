using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DrawableItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public CircleCollider2D BoxCollider2D;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"Begin drag{eventData}");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        BoxCollider2D.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"End drag{eventData}");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        BoxCollider2D.enabled = true;
        GameManager.ModRefresher?.Invoke();
    }


}
