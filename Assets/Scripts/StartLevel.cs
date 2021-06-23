using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            //Se empiezan a mover los enemigos
            EnemyController[] arrayEnemiesControllers = FindObjectsOfType<EnemyController>();
            foreach (EnemyController controller in arrayEnemiesControllers)
            {
                controller.enabled = true;
            }
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
