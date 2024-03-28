using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SizeAdjustAble : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool _isSizeAdjustAble;
    [SerializeField] private GameObject _sizePanel;
    [SerializeField] private TMP_InputField _widthInputField;
    [SerializeField] private TMP_InputField _heightInputField;

    private RectTransform _rectTransform;
    
    private void Start()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
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
            _sizePanel.SetActive(false);
            return;
        }

        Painter.SelectGameObject = gameObject;
        
        _sizePanel.SetActive(true);
        _widthInputField.onValueChanged.RemoveAllListeners();
        _widthInputField.onValueChanged.AddListener(WidthSizeChange);
        
        _heightInputField.onValueChanged.RemoveAllListeners();
        _heightInputField.onValueChanged.AddListener(HeightSizeChange);
        
        _widthInputField.text = $"{_rectTransform.sizeDelta.x}";
        _heightInputField.text = $"{_rectTransform.sizeDelta.y}";
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