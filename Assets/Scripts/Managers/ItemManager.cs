using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    private int _currentItems;
    /*
     * TODO
     * - Añadir el text mesh pro para el texto del número de frutas
     */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Se añaden al valor actual un número determinado de items (nItems)
    /// </summary>
    /// <param name="nItems"></param>
    public void AddItem(int nItems)
    {
        _currentItems += nItems;
        Debug.Log("Total de manzanas: " + _currentItems);
        //TODO: Modificar el texto.
    }

    public void SubItem()
    {
        _currentItems -= 1;
        Debug.Log("Total de manzanas: " + _currentItems);
    }

    public int currentItems
    {
        get { return _currentItems; }
    }
}
