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
    /// Usando collisions detecto que toco el suelo y reseteo el doble salto
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
            /*
             * TODO quitarle vida al personaje
             */
        }
    }
}
