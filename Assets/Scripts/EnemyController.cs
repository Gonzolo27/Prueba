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

    private float dir;  //Determina la direcci�n de movimiento inicial
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
            Debug.Log(collision.contacts[collision.contacts.Length - 1]);
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
    /// Hace que reproduzca la animaci�n de da�o
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
        
        if (attacking && healthManager.enemyType.Equals(HealthManager.EnemyType.BEE) && !_goDown && !_goUp)
        {
            Debug.Log("Entro -1");
            startPositionPlayer = playerManager.transform.position;
            startPositionEnemy = transform.position;
            Debug.Log("Position player = " + startPositionPlayer);
            Debug.Log("Position enemy = " + startPositionEnemy);
            distance = Vector3.Distance(startPositionEnemy, startPositionPlayer);
            Debug.Log(distance);
            _goDown = true;
        }
        if (_goDown && attacking && (distance > 0.01f || !playerHitted) && !_goUp)
        {
            Debug.Log("Entro 0");
            transform.position = Vector2.MoveTowards(this.transform.position, startPositionPlayer, 1 * Time.deltaTime);
            distance = Vector3.Distance(this.transform.position, startPositionPlayer);
        }
        if ((distance < 0.01f || playerHitted) && healthManager.enemyType.Equals(HealthManager.EnemyType.BEE) && attacking && !_goUp)
        {
            attacking = false;
            Debug.Log("Up!!!");
            _goUp = true;

            playerHitted = false;
            _goDown = false;
            //ReturnToPreviousPosition();
        }
        //else if (_goUp && (distance < 0.01f || playerHitted) && healthManager.enemyType.Equals(HealthManager.EnemyType.BEE) && attacking)
        if(_goUp)
        {
            Debug.Log("Entro1");
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

    /*
    private void ReturnToPreviousPosition()
    {
        Debug.Log("Entro 2");
        StartCoroutine(ReturnPrevious());
    }

    private IEnumerator ReturnPrevious()
    {
        Debug.Log("Start Position enemy antes del yield = " + startPositionEnemy);
        Debug.Log("Position enemy antes del yield = " + this.transform.position);
        yield return transform.position = Vector2.MoveTowards(this.transform.position, startPositionEnemy, 1 * Time.deltaTime);
        //Debug.Log("Position enemy despu�s del yield = " + this.transform.position);
        _goUp = false;
    }*/

    private void LateUpdate()
    {
        animator.SetBool(WALKING, walking);
        animator.SetBool(ATTACK, attacking);
    }

}
