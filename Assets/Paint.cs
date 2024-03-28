using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable All

public enum PainterState
{
    Draw,
    Move,
    Rotation,
}

public static class Painter
{
    public static PainterState PainterState = PainterState.Draw; 
    private static readonly Stack<Action> _objectActionUnDoStack = new();
    private static readonly Stack<Action> _objectActionReDoStack = new();

    public static void DoAction(Action action)
    {
        _objectActionReDoStack.Clear();
        _objectActionUnDoStack.Push(action);
    }

    public static void Redo()
    {
        if (!_objectActionReDoStack.TryPop(out var objectActionData))
        {
            return;
        }
                
        _objectActionUnDoStack.Push(objectActionData);
        objectActionData.Redo();
    }

    public static void Undo()
    {
        if (!_objectActionUnDoStack.TryPop(out var objectActionData))
        {
            return;
        }

        _objectActionReDoStack.Push(objectActionData);
        objectActionData.Undo();
    }
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
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
        {
            Painter.Undo();
        }
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
        {
            Painter.Redo();
        }
    }

    public void OnClickMoveButton()
    {
        Painter.PainterState = PainterState.Move;
    }

    public void OnClickRotationButton()
    {
        Painter.PainterState = PainterState.Rotation;
    }
    
    public void OnClickBlackButton()
    {
        Painter.PainterState = PainterState.Draw;
        _lineGenerator.SetColor(LineColor.Black);
        _sizePanel.SetActive(false);
    }

    public void OnClickRedButton()
    {
        Painter.PainterState = PainterState.Draw;
        _lineGenerator.SetColor(LineColor.Red);
        _sizePanel.SetActive(false);
    }

    public void OnClickBlueButton()
    {
        Painter.PainterState = PainterState.Draw;
        _lineGenerator.SetColor(LineColor.Blue);
        _sizePanel.SetActive(false);
    }

    public void OnClickCircleButton()
    {
        Painter.PainterState = PainterState.Move;
        var circle = Instantiate(_dragAbleObject, _paintObjectParent).GetComponent<Image>();
        circle.sprite = Resources.Load<Sprite>("Aoe/CircleAoe");
        circle.gameObject.SetActive(true);
        
        Painter.DoAction(new CreateAction(circle.gameObject));
    }

    public void OnClickLineButton()
    {
        Painter.PainterState = PainterState.Move;
        var line = Instantiate(_dragAbleObject, _paintObjectParent).GetComponent<Image>();
        line.sprite = Resources.Load<Sprite>("Aoe/LineAoe");
        line.gameObject.SetActive(true);
        
        Painter.DoAction(new CreateAction(line.gameObject));
    }
}
