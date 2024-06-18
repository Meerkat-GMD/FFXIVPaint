using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragAble : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private GameObject parentObject;
    [SerializeField]
    private Camera _mainCaemra;
    private Canvas canvas;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("Canvas not found in parent hierarchy.");
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Painter.PainterState != PainterState.Move)
        {
            return;
        }
        
        Painter.DoAction(new MoveAction(parentObject, transform.localPosition));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Painter.PainterState != PainterState.Move)
        {
            return;
        }
        
        parentObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
         
    }

    public void OnDrop(PointerEventData eventData)
    {
    }
}
