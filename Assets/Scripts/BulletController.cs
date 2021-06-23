using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    HealthManager healthManager;
    WeaponDamage weaponDamage;
    PlayerController playerController;

    public int damage;
    public Vector2 m_NewForce;
    public float speed;
    private bool collisioned;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        weaponDamage = GetComponent<WeaponDamage>();
        playerController = FindObjectOfType<PlayerController>();
        ApplyForce();
    }

    public void ApplyForce()
    {
        m_NewForce.x = m_NewForce.x * playerController.GetRight();
        rigidBody2D.AddForce(m_NewForce * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Enemy") && !collisioned)
        {
            collisioned = true;
            healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.GetHit(damage);
            Destroy(this.gameObject);
        }
        if (collision.transform.tag.Equals("Ground"))
        {
            Destroy(this.gameObject);
        }
    }

}
