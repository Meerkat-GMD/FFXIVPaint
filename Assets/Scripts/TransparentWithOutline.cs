using UnityEngine;
using UnityEngine.UI;

public class TransparentWithOutline : MonoBehaviour
{
    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // 이미지 투명하게 설정
            Color color = image.color;
            color.a = 0;
            image.color = color;
        }

        Outline outline = GetComponent<Outline>();
        if (outline != null)
        {
            // 테두리 설정
            outline.effectColor = Color.black;
            outline.effectDistance = new Vector2(1, -1);
        }
    }
}