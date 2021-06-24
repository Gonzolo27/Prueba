using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    EnemyController enemyController;
    KillEnemyManager killEnemyManager;
    public GameObject killEnemyUFX;
    public enum EnemyType { CHICKEN, BEE };
    public EnemyType enemyType;

    public int lifes;

    private IEnumerator coroutine;
    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        killEnemyManager = FindObjectOfType<KillEnemyManager>();
    }

    /// <summary>
    /// Se le resta vida al enemigo en función del daño
    /// pudiendo morir si tiene menos de 0 de vida.
    /// </summary>
    /// <param name="damage">Daño causado al enemigo</param>
    public void GetHit(int damage)
    {
        enemyController.SetAnimatorDamage(true);
        lifes -= damage;
        if (lifes <= 0)
        {
            switch (enemyType)
            {
                case EnemyType.BEE:
                    SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.BEEDEAD);
                    this.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
                    break;
                case EnemyType.CHICKEN:
                    SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.CHICKENDEAD);
                    break;
            }

            enemyController.killed = true;
            enemyController.StopWalk(false);
            killEnemyManager.KillEnemy(this.transform, killEnemyUFX);
            coroutine = DestroyAfterTime(1f);
            this.GetComponentInChildren<BoxCollider2D>().enabled = false;
            StartCoroutine(coroutine);
            
        }
        else
        {
            Invoke("RemoveHit", 0.2f);
        }
            
    }

    /// <summary>
    /// Hace que cambie la animación
    /// </summary>
    private void RemoveHit()
    {
        enemyController.SetAnimatorDamage(false);
    }

    /// <summary>
    /// Se destruye el enemigo y su zona de patruya tras un periodo de tiempo
    /// </summary>
    /// <param name="time">tiempo de espera para destruir al enemigo</param>
    /// <returns></returns>
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(enemyController.patrolZone.gameObject);
        Destroy(this.gameObject);
    }
}
