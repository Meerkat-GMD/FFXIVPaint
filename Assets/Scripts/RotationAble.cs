using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class RotationAble : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        private bool _isRotationAble;
        public void Init(bool isRotationAble)
        {
            _isRotationAble = isRotationAble;
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

            _previousMousePos = Camera.main.WorldToScreenPoint(_rectTransform.position);
            Vector2 vec2 = eventData.position - _previousMousePos;
            _angleOffset = (Mathf.Atan2(_rectTransform.right.y, _rectTransform.right.x) - Mathf.Atan2(vec2.y, vec2.x)) * Mathf.Rad2Deg;
            
            Painter.DoAction(new RotateAction(gameObject, _rectTransform.eulerAngles.z));
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