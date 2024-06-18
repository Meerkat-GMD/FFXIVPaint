using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class ResizeUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
    {
        public RectTransform rectTransform;
        private Vector2 originalSizeDelta;
        private Vector2 originalMousePosition;
        private Vector2 startMousePosition;
        bool _isSizeAdjustAble;

        public void Init(bool isSizeAdjustAble)
        {
            _isSizeAdjustAble = isSizeAdjustAble;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isSizeAdjustAble)
            {
                Paint.Instance.SizePanel.SetActive(false);
                return;
            }

            Paint.Instance.SizePanel.SetActive(true);
            originalSizeDelta = rectTransform.sizeDelta;
            originalMousePosition = eventData.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalSizeDelta = rectTransform.sizeDelta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out originalMousePosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.pressPosition, eventData.pressEventCamera, out startMousePosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            
            Vector2 currentMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out currentMousePosition);

            // 시작 점과 현재 점의 차이를 구함
            Vector2 diff = currentMousePosition - startMousePosition;

            // 시작점과 현재 점의 차이에 따라 크기 조절
            Vector2 newSizeDelta = originalSizeDelta + diff;

            // 크기는 음수가 될 수 없도록 조정
            newSizeDelta.x = Mathf.Max(newSizeDelta.x, 0f);
            newSizeDelta.y = Mathf.Max(newSizeDelta.y, 0f);

            rectTransform.sizeDelta = newSizeDelta;
        }
    }
}