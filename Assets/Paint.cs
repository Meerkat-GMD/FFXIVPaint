using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum InputState
{
    Move,
    Draw,
}

public static class Painter
{
    public static bool IsDrawingState { get; set; } = true;

    public static Stack<Action> ObjectActionUnDoStack = new();
    public static Stack<Action> ObjectActionReDoStack = new();
}

public class Paint : MonoBehaviour
{
    [SerializeField] private LineGenerator _lineGenerator;
    [SerializeField] private Transform _paintObjectParent;
    [SerializeField] private GameObject _sizePanel;
    [SerializeField] private GameObject _dragAbleObject;
    
    private void Start()
    {
        _lineGenerator.Init(_paintObjectParent);
        _sizePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            
        }
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
        {
            if (Painter.IsDrawingState)
            {
                _lineGenerator.Undo();
            }
            else
            {
                if (!Painter.ObjectActionUnDoStack.TryPop(out var objectActionData))
                {
                    return;
                }

                Painter.ObjectActionReDoStack.Push(objectActionData);
                objectActionData.Undo();
            }
        }
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Y))
        {
            if (Painter.IsDrawingState)
            {
                _lineGenerator.Redo();
            }
            else
            {
                if (!Painter.ObjectActionReDoStack.TryPop(out var objectActionData))
                {
                    return;
                }
                
                Painter.ObjectActionUnDoStack.Push(objectActionData);
                objectActionData.Redo();
            }
        }
    }

    public void OnClickPenButton()
    {
        Painter.IsDrawingState = false;
    }
    
    public void OnClickBlackButton()
    {
        Painter.IsDrawingState = true;
        _lineGenerator.SetColor(LineColor.Black);
        _sizePanel.SetActive(false);
    }

    public void OnClickRedButton()
    {
        Painter.IsDrawingState = true;
        _lineGenerator.SetColor(LineColor.Red);
        _sizePanel.SetActive(false);
    }

    public void OnClickBlueButton()
    {
        Painter.IsDrawingState = true;
        _lineGenerator.SetColor(LineColor.Blue);
        _sizePanel.SetActive(false);
    }

    public void OnClickCircleButton()
    {
        Painter.IsDrawingState = false;
        var circle = Instantiate(_dragAbleObject, _paintObjectParent).GetComponent<Image>();
        circle.sprite = Resources.Load<Sprite>("Aoe/CircleAoe");
        circle.gameObject.SetActive(true);
        
        Painter.ObjectActionReDoStack.Clear();
        Painter.ObjectActionUnDoStack.Push(new CreateAction(circle.gameObject));
    }
}
