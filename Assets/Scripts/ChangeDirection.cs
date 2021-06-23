using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    public GameObject enemy;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemy != null)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            if (collision.transform.name.Equals(enemy.name) && !enemyController.killed)
            {
                enemy.transform.Rotate(0.0f, 180.0f, 0.0f);
                enemyController.StopWalk(true);
            }
        }
        
    }

}
