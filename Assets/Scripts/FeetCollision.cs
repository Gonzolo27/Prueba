using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollision : MonoBehaviour
{
    private PlayerManager playerManager;
    public int damage;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    /// <summary>
    /// Usando collisions se detecta que toco el suelo y se resetea el doble salto
    /// También se comprueba si salta encima de un enemigo para dañarlo o cae en un trampa en la que el personaje sufre daño
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Ground"))
        {
            playerManager.ground = true;
            playerManager.doubleJumpDid = false;
            playerManager.doubleJump = false;
            playerManager.fall = false;
        }
        if (collision.transform.tag.Equals("Enemy"))
        {
            SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.HIT);
            playerManager.DoImpulse(playerManager.hitVerticalForce);
            playerManager.hitEnemy = true;
            collision.gameObject.GetComponent<HealthManager>().GetHit(damage);
        }
        if (collision.transform.tag.Equals("Trap"))
        {
            SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.PLAYERHITTED);
            int damage = collision.transform.GetComponent<WeaponDamage>().damage;
            FindObjectOfType<HealthManagerPlayer>().MakeDamage(damage);
            playerManager.collisioned = true;
            playerManager.ground = true;
        }
    }
}
