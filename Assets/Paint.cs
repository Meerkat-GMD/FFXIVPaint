using UnityEngine;

public enum InputState
{
    Move,
    Draw,
}

public static class Painter
{
    public static bool IsDrawingState { get; set; } = true;
}

public class Paint : MonoBehaviour
{
    [SerializeField] private LineGenerator _lineGenerator;

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
