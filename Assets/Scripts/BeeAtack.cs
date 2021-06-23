using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAtack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            EnemyController enemyController = GetComponentInParent<EnemyController>();
            enemyController.GetComponentInChildren<Animator>().applyRootMotion = false;
            enemyController.attacking = true;
            enemyController.StopWalk(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            EnemyController enemyController = GetComponentInParent<EnemyController>();
            enemyController.GetComponentInChildren<Animator>().applyRootMotion = true;
            enemyController.attacking = false;
            enemyController.SetWalk();
        }
    }
}
