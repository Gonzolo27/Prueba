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

    /// <summary>
    /// Modifica el color de los botones del menú principal al posicionarse el cursor en él
    /// </summary>
    public void PointerEnter()
    {
        text.color = m_MouseOverColor;
    }

    /// <summary>
    /// Modifica el color de los botones del menú principal al salir el cursor de él
    /// </summary>
    public void PointerExit()
    {
        text.color = m_OriginalColor;
    }
}
