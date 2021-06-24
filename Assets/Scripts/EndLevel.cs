using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public enum tipoFin {Win, Dead};
    public tipoFin tipo;

    /// <summary>
    /// Si el tipo es Dead, el jugador muere
    /// Si el tipo es Win, el jugador gana 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            switch (tipo)
            {
                case tipoFin.Win:
                    FindObjectOfType<UIManager>().Win();
                    break;
                case tipoFin.Dead:
                    FindObjectOfType<UIManager>().Dead();
                    break;
            }
            
        }
    }
}
