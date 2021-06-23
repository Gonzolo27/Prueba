using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    EnemyController enemyController;
    KillEnemyManager killEnemyManager;
    public GameObject killEnemyUFX;

    public int lifes;

    private IEnumerator coroutine;
    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        killEnemyManager = FindObjectOfType<KillEnemyManager>();
    }
    public void GetHit(int damage)
    {
        enemyController.SetAnimatorDamage(true);
        lifes -= damage;
        if (lifes <= 0)
        {
            enemyController.killed = true;
            enemyController.StopWalk(false);
            killEnemyManager.KillEnemy(this.transform, killEnemyUFX);
            coroutine = DestroyAfterTime(1f);
            this.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(coroutine);
            
        }
        else
        {
            Invoke("RemoveHit", 0.2f);
        }
            
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(enemyController.patrolZone.gameObject);
        Destroy(this.gameObject);
    }

    public void RemoveHit()
    {
        enemyController.SetAnimatorDamage(false);
    }

    public void ShowUFXKilled(Transform positionItem)
    {
        //Destroy(Instantiate(pickUpItemUFX, positionItem.position, positionItem.rotation), 1.0f);
        Destroy(Instantiate(killEnemyUFX, gameObject.transform), 1.0f);
    }
}
