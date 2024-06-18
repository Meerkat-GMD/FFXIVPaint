using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DragAbleObject : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private bool _isSizeAdjustAble;
        [SerializeField] private bool _isRotationAble;
        
        [SerializeField] private ResizeUI _resizeUI;
        [SerializeField] private SizeAdjustAble _sizeAdjustAble;
        [SerializeField] private RotationAble _rotationAble;
        
        public void Start()
        {
            _resizeUI.Init(_isSizeAdjustAble);
            _sizeAdjustAble.Init(_isSizeAdjustAble);
            _rotationAble.Init(_isRotationAble);
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}