using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private PlayerManager playerManager;
    private HealthManagerPlayer healthManagerPlayer;
    public float speed;
    private bool walking = true;
    public Vector3 hitHorizontalForce;
    public BoxCollider2D patrolZone;
    private Animator animator;
    public bool killed;
    public bool attacking;

    private float dir;  //Determina la dirección de movimiento inicial
    public enum StartDirection { Left, Right};
    public StartDirection startDirection;

    private const string DAMAGE = "Damage", WALKING = "Walking", ATTACK = "Attack";
    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        healthManagerPlayer = FindObjectOfType<HealthManagerPlayer>();
        animator = GetComponentInChildren<Animator>();
        SetDir();
    }

    protected void SetDir()
    {
        dir = 1.0f;
        if (startDirection == StartDirection.Left)
        {
            dir = -1.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player") && !playerManager.collisioned && !playerManager.hitEnemy && collision.collider.name.Equals("Player"))
        {
            SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.PLAYERHITTED);
            healthManagerPlayer.MakeDamage();
            Vector3 impulse = hitHorizontalForce;
            if (collision.contacts[collision.contacts.Length - 1].normal.x > 0)
            {
                impulse = -impulse;
            }
            playerManager.DoImpulse(impulse);
            playerManager.collisioned = true;
            StopWalk(true);
        }
    }

    protected void Walk()
    {
        Vector3 translation = new Vector3(dir * speed * Time.deltaTime, 0, 0);
        this.transform.Translate(translation);
    }

    public void SetWalk()
    {
        walking = true;
    }

    public void StopWalk(bool walkAgain)
    {
        walking = false;
        if (walkAgain && !killed && !attacking)
        {
            Invoke("SetWalk", Random.Range(0.3f, 0.8f));
        }
            
    }

    public void SetAnimatorDamage(bool ok)
    {
        animator.SetBool(DAMAGE, ok);
    }
    void Update()
    {
        if (walking)
        {
            Walk();
        }
    }

    private void LateUpdate()
    {
        animator.SetBool(WALKING, walking);
        animator.SetBool(ATTACK, attacking);
    }

}
