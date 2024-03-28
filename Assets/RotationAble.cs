using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class RotationAble : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private bool _isRotationAble;
        
        private RectTransform _rectTransform;
    
        private void Start()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Painter.PainterState != PainterState.Rotation)
            {
                return;
            }

            if (!_isRotationAble)
            {
                return;
            }

            _previousMousePos = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 vec2 = eventData.position - _previousMousePos;
            _angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec2.y, vec2.x)) * Mathf.Rad2Deg;
            
            Painter.DoAction(new RotateAction(gameObject, transform.eulerAngles.z));
        }

        private Vector2 _previousMousePos;
        private float _angleOffset;
        public void OnDrag(PointerEventData eventData)
        {
            if (Painter.PainterState != PainterState.Rotation)
            {
                return;
            }
            
            if (!_isRotationAble)
            {
                return;
            }

            var direction = eventData.position - _previousMousePos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rectTransform.eulerAngles = new Vector3(0, 0, angle + _angleOffset);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrop(PointerEventData eventData)
        {
            
        }
    }
}