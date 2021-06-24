using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAtack : MonoBehaviour
{
    /// <summary>
    /// Si el player entra en la zona de ataque, la abeja para de patruyar y ataca con una animación
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemyController = GetComponentInParent<EnemyController>();
        if (collision.transform.tag.Equals("Player") && !enemyController.goDown && !enemyController.goDown)
        {
            enemyController.GetComponentInChildren<Animator>().applyRootMotion = false;
            enemyController.attacking = true;
            enemyController.StopWalk(false);
        }
    }

    /// <summary>
    /// Si el player sale de la zona de ataque y no está ni subiendo ni bajando, la abeja vuelve a patrullar
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyController enemyController = GetComponentInParent<EnemyController>();
        if (collision.transform.tag.Equals("Player") && !enemyController.goDown && !enemyController.goDown)
        {
            enemyController.GetComponentInChildren<Animator>().applyRootMotion = true;
            enemyController.SetWalk();
        }
    }
}
