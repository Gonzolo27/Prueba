using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnMouseOverStart : MonoBehaviour
{
    Color m_MouseOverColor = Color.white;
    Color m_OriginalColor;
    //CanvasRenderer m_Renderer;
    public TMP_Text text;

    void Start()
    {
        m_OriginalColor = text.color;
    }

    public void PointerEnter()
    {
        text.color = m_MouseOverColor;
    }

   /* public void OnPointerEnter(PointerEventData pointerEventData)
    {
        text.color = m_MouseOverColor;
    }*/

    public void PointerExit()
    {
        text.color = m_OriginalColor;
    }
}
