using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragAble : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    private Camera _mainCaemra;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Painter.IsDrawingState)
        {
            return;
        }
        
        Painter.ObjectActionReDoStack.Clear();
        Painter.ObjectActionUnDoStack.Push(new MoveAction(gameObject, transform.localPosition));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Painter.IsDrawingState)
        {
            return;
        }

        var data = _mainCaemra.ScreenToWorldPoint(eventData.position);
        data = new Vector2(data.x, data.y);
        transform.position = data;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
    }
}
