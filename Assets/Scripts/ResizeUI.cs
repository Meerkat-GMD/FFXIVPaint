using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public enum DragState
    {
        Left,
        Right,
        Up,
        Down,
        LeftUp,
        RightUp,
        LeftDown,
        RightDown,
    }

    public class ResizeUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler
    {
        public Texture2D _resizeAbleCursorVertical;
        public Texture2D _resizeAbleCursorHorizon;
        public Texture2D _resizeAbleCursorTopLeft;
        public Texture2D _resizeAbleCursorTopRight;
        public RectTransform rectTransform;
        private Vector2 originalSizeDelta;
        private Vector2 originalMousePosition;
        private Vector2 startMousePosition;
        bool _isSizeAdjustAble;

        private Vector2 GetAnchorVectorByDargState(DragState dragState)
        {
            return dragState switch
            {
                DragState.Left => new Vector2(0, 0.5f),
                DragState.Right => new Vector2(1, 0.5f),
                DragState.Up => (new Vector2(0.5f, 1)),
                DragState.Down => (new Vector2(0.5f, 0)),
                DragState.LeftUp => (new Vector2(0, 1)),
                DragState.RightUp => (new Vector2(1, 1)),
                DragState.LeftDown => (new Vector2(0, 0)),
                DragState.RightDown => (new Vector2(1, 0)),
                _ => throw new System.NotImplementedException(),
            };
        }

        private Texture2D GetCursorImage(DragState dragState)
        {
            return dragState switch
            {
                DragState.Left => _resizeAbleCursorHorizon,
                DragState.Right => _resizeAbleCursorHorizon,
                DragState.Up => _resizeAbleCursorVertical,
                DragState.Down => _resizeAbleCursorVertical,
                DragState.LeftUp => _resizeAbleCursorTopLeft,
                DragState.RightUp => _resizeAbleCursorTopRight,
                DragState.LeftDown => _resizeAbleCursorTopRight,
                DragState.RightDown => _resizeAbleCursorTopLeft,
                _ => throw new System.NotImplementedException(),
            };
        }

        public void Init(bool isSizeAdjustAble)
        {
            _isSizeAdjustAble = isSizeAdjustAble;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var cursorImage = GetCursorImage(DragState.LeftDown);
            Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width/2, cursorImage.height/2), CursorMode.Auto);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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

            var temp = rectTransform.localPosition;
            rectTransform.pivot = GetAnchorVectorByDargState(DragState.LeftDown);
            rectTransform.localPosition = temp;

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

        public void OnEndDrag(PointerEventData eventData)
        {
            var temp = rectTransform.localPosition;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.localPosition = temp;
        }
    }
}