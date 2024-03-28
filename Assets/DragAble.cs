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
        if (Painter.PainterState != PainterState.Move)
        {
            return;
        }
        
        Painter.DoAction(new MoveAction(gameObject, transform.localPosition));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Painter.PainterState != PainterState.Move)
        {
            return;
        }

        var data = _mainCaemra.ScreenToWorldPoint(eventData.position);
        data = new Vector3(data.x, data.y, 0);
        transform.position = data;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
    }
}
