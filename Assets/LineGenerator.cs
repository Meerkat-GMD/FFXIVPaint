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

    private Line activeLine;
    private LineColor _lineColor;
    
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
            var newLine = Instantiate(LinePrefab);
            newLine.GetComponent<LineRenderer>().material = _lineColor switch
            {
                LineColor.Black => BlackLineMaterial,
                LineColor.Red => RedLineMaterial,
                LineColor.Blue => BlueLineMaterial,
                _ => throw new ArgumentOutOfRangeException(),
            };
            activeLine = newLine.GetComponent<Line>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }

        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
    }

    
    
    public void SetColor(LineColor lineColor)
    {
        _lineColor = lineColor;
    }
}
