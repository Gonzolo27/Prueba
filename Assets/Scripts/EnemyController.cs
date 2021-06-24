using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private PlayerManager playerManager;
    private HealthManagerPlayer healthManagerPlayer;
    private HealthManager healthManager;
    public float speed;
    private bool walking = true;
    public Vector3 hitHorizontalForce;
    public BoxCollider2D patrolZone;
    private Animator animator;
    public bool killed;
    public bool attacking;
    public int damage;

    private float dir;  //Determina la dirección de movimiento inicial
    public enum StartDirection { Left, Right };
    public StartDirection startDirection;

    private Vector3 startPositionPlayer;
    private Vector3 startPositionEnemy;
    private bool _goDown;
    private bool _goUp;
    private bool playerHitted;

    [SerializeField] float distance;

    private const string DAMAGE = "Damage", WALKING = "Walking", ATTACK = "Attack";
    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        healthManagerPlayer = FindObjectOfType<HealthManagerPlayer>();
        healthManager = this.gameObject.GetComponent<HealthManager>();
        animator = GetComponentInChildren<Animator>();
        SetDir();
    }

    public bool goDown
    {
        get{return _goDown;}
    }

    public bool goUp
    {
        get { return _goUp; }
    }

    /// <summary>
    /// Se setea el sentido en el que se mueve el enemigo
    /// </summary>
    protected void SetDir()
    {
        dir = 1.0f;
        if (startDirection == StartDirection.Left)
        {
            dir = -1.0f;
        }
    }

    /// <summary>
    /// Si se cumplen las condiciones se produce un impacto en el player
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player") && !playerManager.collisioned && !playerManager.hitEnemy && collision.collider.name.Equals("Player"))
        {
            if (healthManager.enemyType.Equals(HealthManager.EnemyType.BEE))
            {
                playerHitted = true;
                _goUp = true;
            }
            else
            {
                StopWalk(true);
            }
            SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.PLAYERHITTED);
            healthManagerPlayer.MakeDamage(damage);
            Vector3 impulse = hitHorizontalForce;
            if (collision.contacts[collision.contacts.Length - 1].normal.x > 0)
            {
                impulse = -impulse;
            }
            playerManager.DoImpulse(impulse);
            playerManager.collisioned = true;
            
        }
    }

    /// <summary>
    /// Hace que se mueva el enemigo
    /// </summary>
    protected void Walk()
    {
        Vector3 translation = new Vector3(dir * speed * Time.deltaTime, 0, 0);
        this.transform.Translate(translation);
    }

    /// <summary>
    /// Hace que el enemigo empiece a moverse
    /// </summary>
    public void SetWalk()
    {
        walking = true;
    }

    /// <summary>
    /// Hace que el enemigo deje de andar entre 0.3 y 0.8 segundos si walkAgain es true
    /// </summary>
    /// <param name="walkAgain"></param>
    public void StopWalk(bool walkAgain)
    {
        walking = false;
        if (walkAgain && !killed && !attacking)
        {
            Invoke("SetWalk", Random.Range(0.3f, 0.8f));
        }
            
    }

    /// <summary>
    /// Hace que reproduzca la animación de daño
    /// </summary>
    /// <param name="ok"></param>
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
        
        //Primer paso, la abeja capta al player y calcula la distancia a la que está y guarda su posición
        if (attacking && healthManager.enemyType.Equals(HealthManager.EnemyType.BEE) && !_goDown && !_goUp && healthManager.lifes > 0)
        {
            startPositionPlayer = playerManager.transform.position;
            startPositionEnemy = transform.position;
            distance = Vector3.Distance(startPositionEnemy, startPositionPlayer);
            _goDown = true;
        }
        //Segundo paso, la abeja va a la última posición conocida del player
        if (_goDown && attacking && (distance > 0.01f || !playerHitted) && !_goUp && healthManager.lifes > 0)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, startPositionPlayer, 1 * Time.deltaTime);
            distance = Vector3.Distance(this.transform.position, startPositionPlayer);
        }
        //Tercer paso, cuando llega a la posición (más o menos) o le alcanza, la abeja tiene que empezar a subir
        if ((distance < 0.01f || playerHitted) && healthManager.enemyType.Equals(HealthManager.EnemyType.BEE) && attacking && !_goUp && healthManager.lifes > 0)
        {
            attacking = false;
            _goUp = true;

            playerHitted = false;
            _goDown = false;
        }
        //Cuarto paso, la abeja empieza a subir hasta alcanzar la posición de la que ha partido hacia abajo
        if(_goUp && healthManager.lifes > 0)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, startPositionEnemy, 0.6f * Time.deltaTime);
            float d = Vector2.Distance(startPositionEnemy, this.transform.position);
            if (d < 0.01f)
            {
                _goUp = false;
                this.transform.GetComponentInChildren<Animator>().applyRootMotion = true;
                walking = true;
            }
        }
    }


    private void LateUpdate()
    {
        animator.SetBool(WALKING, walking);
        animator.SetBool(ATTACK, attacking);
    }

}
