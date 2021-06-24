using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    /// <summary>
    /// Cuando empieza el nivel, los enemigos empiezan a moverse y se desactiva este collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            EnemyController[] arrayEnemiesControllers = FindObjectsOfType<EnemyController>();
            foreach (EnemyController controller in arrayEnemiesControllers)
            {
                controller.enabled = true;
            }
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
