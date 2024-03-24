using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineColor
{
    Black,
    Red,
    Blue,
}

public class LineGenerator : MonoBehaviour
{
    public GameObject LinePrefab;
    public Material BlackLineMaterial;
    public Material RedLineMaterial;
    public Material BlueLineMaterial;

    private Line _activeLine;
    private readonly Stack<Line> _undoLineStack = new();
    private readonly Stack<Line> _redoLineStack = new();
    private LineColor _lineColor;
    private Transform _paintObjectParent;
    public void Init(Transform paintObjectParent)
    {
        _paintObjectParent = paintObjectParent;
    }
    
    private void Start()
    {
        _lineColor = LineColor.Black;
    }
    
    void Update()
    {
        if (!Painter.IsDrawingState)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            var newLine = Instantiate(LinePrefab, _paintObjectParent);
            newLine.GetComponent<LineRenderer>().material = _lineColor switch
            {
                LineColor.Black => BlackLineMaterial,
                LineColor.Red => RedLineMaterial,
                LineColor.Blue => BlueLineMaterial,
                _ => throw new ArgumentOutOfRangeException(),
            };
            _activeLine = newLine.GetComponent<Line>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _undoLineStack.Push(_activeLine);
            foreach (var redoLine in _redoLineStack)
            {
                Destroy(redoLine);
            }
            _redoLineStack.Clear();
            _activeLine = null;
        }

        if (_activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _activeLine.UpdateLine(mousePos);
        }
    }
    
    public void SetColor(LineColor lineColor)
    {
        _lineColor = lineColor;
    }

    public void Undo()
    {
        if (!_undoLineStack.TryPop(out var undoLine))
        {
            return;
        }
        
        undoLine.gameObject.SetActive(false);
        
        _redoLineStack.Push(undoLine);
    }

    public void Redo()
    {
        if (!_redoLineStack.TryPop(out var redoLine))
        {
            return;
        }
        
        redoLine.gameObject.SetActive(true);
        
        _undoLineStack.Push(redoLine);
    }
}
