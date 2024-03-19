using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum InputState
{
    Move,
    Draw,
}

public static class Painter
{
    public static bool IsDrawingState { get; set; } = true;

    public static Stack<(GameObject, Vector3)> DragUnDoStack = new();
    public static Stack<(GameObject, Vector3)> DragReDoStack = new();
}

public class Paint : MonoBehaviour
{
    [SerializeField] private LineGenerator _lineGenerator;

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
                if (!Painter.DragUnDoStack.TryPop(out var dragData))
                {
                    return;
                }

                var obj = dragData.Item1;
                var localPosition = dragData.Item2;

                Painter.DragReDoStack.Push((obj, obj.transform.localPosition));
                obj.transform.localPosition = localPosition;
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
                if (!Painter.DragReDoStack.TryPop(out var dragData))
                {
                    return;
                }
                
                var obj = dragData.Item1;
                var localPosition = dragData.Item2;
                
                Painter.DragUnDoStack.Push((obj, obj.transform.localPosition));
                obj.transform.localPosition = localPosition;
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
    }

    public void OnClickRedButton()
    {
        Painter.IsDrawingState = true;
        _lineGenerator.SetColor(LineColor.Red);
    }

    public void OnClickBlueButton()
    {
        Painter.IsDrawingState = true;
        _lineGenerator.SetColor(LineColor.Blue);
    }
}
