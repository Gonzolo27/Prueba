using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private PlayerManager playerManager;
    private ItemManager itemManager;
    public GameObject shootPoint;
    public GameObject bullet;

    private const string AXIS_H = "Horizontal", LAST_H = "WalkingRight", WALKING = "Walking", GROUND = "OnGround";
    private const string JUMP = "Jump", D_JUMP = "DoubleJump", LAST_M = "LastMovement", FALL = "Fall", D_JUMPDID = "DoubleJumpDid";
    private const string DAMAGE = "Damage", STOP = "Stop";


    [SerializeField] private float speed = 2.0f;                //Determina la velocidad de movimiento del personaje
    [SerializeField] private bool walking = false;              //Determina si el personaje está en movimiento
    [SerializeField] private float lastMovement = 0.0f;         //Almacena el último valor de movimiento para saber la dirección
    private bool right = true;                                  //Si lastMovement es > 0 el personaje mirará a derecha sino, a izquierda
    private bool stop;

    private float shootPointPosX;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerManager = FindObjectOfType<PlayerManager>();
        itemManager = FindObjectOfType<ItemManager>();
        _animator = GetComponent<Animator>();
        lastMovement = 1;
        shootPointPosX = shootPoint.transform.localPosition.x;
    }


    void Update()
    {

        walking = false;
        if (!playerManager.collisioned)
        {
            /*Si no lo compruebo en el siguiente frame a iniciar el doble salto
             *no hace la animación del doble salto si se pulsa cuando está en falling
             */
            if (playerManager.doubleJump)
            {
                playerManager.doubleJumpDid = true;
            }
            if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f)
            {
                float movement = Input.GetAxis(AXIS_H);
                Walk(movement);
            }
            if (Input.GetButtonDown("Jump"))
            {
                
                //Primer impulso
                if (playerManager.ground == true)
                {
                    walking = false;
                    SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.JUMP);
                    Jump();
                    playerManager.jump = true;
                }
                //Segundo impulso
                else if ((playerManager.jump == true || playerManager.fall == true) && playerManager.doubleJumpDid == false)
                {
                    walking = false;
                    SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.JUMP);
                    Jump();
                    playerManager.doubleJump = true;
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (itemManager.currentItems > 0)
                {
                    SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.THROW);
                    Debug.Log(shootPoint.transform.position);
                    Instantiate(bullet, shootPoint.transform);
                    itemManager.SubItem();
                }
            }
        }
        if (walking && !SFXManager.SharedInstance.IsPlayingSteps())
        {
            Debug.Log("Empiezo a sonar");
            SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.STEPS);
        }
        //else if ((!walking || (playerManager.jump || playerManager.fall)) && SFXManager.SharedInstance.IsPlayingSteps())
        else if (!playerManager.ground && SFXManager.SharedInstance.IsPlayingSteps())
        {
            //Podría hacerse también sabiendo si estoy o no tocando el suelo.
            Debug.Log("Paro de sonar");
            SFXManager.SharedInstance.StopSteps();
        }

    }

    public void DeactivatePlayer()
    {
        stop = true;
        this.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.transform.GetComponentInChildren<BoxCollider2D>().enabled = false;
        this.transform.GetComponentInChildren<CircleCollider2D>().enabled = false;

    }

    private void Walk(float movement)
    {
        Vector3 translation = new Vector3(movement * speed * Time.deltaTime, 0, 0);
        lastMovement = translation.x;
        this.transform.Translate(translation);
        walking = true;
    }

    public int GetRight()
    {
        int x = -1;
        if (right)
        {
            x = 1;
        }

        return x;
    }

    private void Jump()
    {
        // _rigidbody2D.AddForce(playerManager.jumpForce, ForceMode2D.Impulse);
        _rigidbody2D.velocity = Vector2.up * playerManager.jumpForce;
        playerManager.fall = false;
        playerManager.ground = false;
    }

    private void FixedUpdate()
    {
        float velocidad_y = _rigidbody2D.velocity.y;
        /* Mediante la velocidad del rigid body detecto que estoy cayendo
         * Si lo pongo a menor que 0, produce errores porque se activa cuando no debería
         * y no entra en otras animaciones
         */
        if (velocidad_y < -0.3f)
        {
            playerManager.ground = false;
            playerManager.fall = true;
            playerManager.jump = false;
        }
    }

    private void LateUpdate()
    {
        _animator.SetFloat(AXIS_H, Input.GetAxis(AXIS_H));
        if (lastMovement < 0)
        {
            right = false;
            float posX = shootPoint.transform.position.x;
            shootPoint.transform.localPosition = new Vector3(-shootPointPosX, 0, 0);
        }
        else
        {
            right = true;
            shootPoint.transform.localPosition = new Vector3(shootPointPosX, 0, 0);
        }
        _animator.SetBool(LAST_H, right);
        _animator.SetBool(JUMP, playerManager.jump);
        _animator.SetBool(D_JUMP, playerManager.doubleJump);
        _animator.SetBool(D_JUMPDID, playerManager.doubleJumpDid);
        _animator.SetBool(WALKING, walking);
        _animator.SetFloat(LAST_M, lastMovement);
        _animator.SetBool(FALL, playerManager.fall);
        _animator.SetBool(GROUND, playerManager.ground);
        _animator.SetBool(DAMAGE, playerManager.collisioned);
        _animator.SetBool(STOP, stop);
        if (stop == true)
        {
            this.enabled = false;
        }
    }
}
