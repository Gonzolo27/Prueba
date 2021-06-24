using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    private int _currentItems;
    public TMP_Text bulletText;

    /// <summary>
    /// Se añaden al valor actual un número determinado de items (nItems)
    /// </summary>
    /// <param name="nItems">número de ítems añadidos</param>
    public void AddItem(int nItems)
    {
        _currentItems += nItems;
        SetText();
    }

    /// <summary>
    /// Cambia el texto del número de items
    /// </summary>
    private void SetText()
    {
        bulletText.text = "x" + _currentItems.ToString();
    }
    /// <summary>
    /// Resta un item al lanzarlo
    /// </summary>
    public void SubItem()
    {
        _currentItems -= 1;
        SetText();
    }

    public int currentItems
    {
        get { return _currentItems; }
    }
}
