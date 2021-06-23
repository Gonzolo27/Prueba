using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    private int _currentItems;
    public TMP_Text bulletText;
    /*
     * TODO
     * - A�adir el text mesh pro para el texto del n�mero de frutas
     */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Se a�aden al valor actual un n�mero determinado de items (nItems)
    /// </summary>
    /// <param name="nItems"></param>
    public void AddItem(int nItems)
    {
        _currentItems += nItems;
        SetText();
        Debug.Log("Total de manzanas: " + _currentItems);
        //TODO: Modificar el texto.
    }

    private void SetText()
    {
        bulletText.text = "x" + _currentItems.ToString();
    }
    public void SubItem()
    {
        _currentItems -= 1;
        SetText();
        Debug.Log("Total de manzanas: " + _currentItems);
    }

    public int currentItems
    {
        get { return _currentItems; }
    }
}
