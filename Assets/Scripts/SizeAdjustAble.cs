using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SizeAdjustAble : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private RectTransform _rectTransform;
    bool _isSizeAdjustAble;
    
    public void Init(bool isSizeAdjustAble)
    {
        _isSizeAdjustAble = isSizeAdjustAble;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Painter.PainterState != PainterState.Move)
        {
            return;
        }
        
        if (!_isSizeAdjustAble)
        {
            Painter.SelectGameObject = null;
            Paint.Instance.SizePanel.SetActive(false);
            return;
        }

        Painter.SelectGameObject = gameObject;
        
        Paint.Instance.SizePanel.SetActive(true);
        Paint.Instance.WidthInputField.onValueChanged.RemoveAllListeners();
        Paint.Instance.WidthInputField.onValueChanged.AddListener(WidthSizeChange);
        
        Paint.Instance.HeightInputField.onValueChanged.RemoveAllListeners();
        Paint.Instance.HeightInputField.onValueChanged.AddListener(HeightSizeChange);
        
        Paint.Instance.WidthInputField.text = $"{_rectTransform.sizeDelta.x}";
        Paint.Instance.HeightInputField.text = $"{_rectTransform.sizeDelta.y}";
    }

    private void WidthSizeChange(string value)
    {
        if (!float.TryParse(value, out float floatValue))
        {
            return;
        }

        _rectTransform.sizeDelta = new Vector2(floatValue, _rectTransform.sizeDelta.y);
    }
    
    private void HeightSizeChange(string value)
    {
        if (!float.TryParse(value, out float floatValue))
        {
            return;
        }

        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, floatValue);
    }
}