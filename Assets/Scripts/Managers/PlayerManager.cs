using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject pickUpItemUFX;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    public Vector3 jumpForce;                                          
    public Vector3 hitVerticalForce;
    public Dictionary<string, Vector3> forces;

    public bool fall = false, ground = true;
    public bool jump = false, doubleJump = false, doubleJumpDid = false;

    public float timeUntilBeCollisioned = 1.0f;
    public float timeCollisioned = 0.0f;
    public bool collisioned = false;

    public float timeUntilHitEnemyAgain = 0.5f;
    public float timeHitEnemy = 0.0f;
    public bool hitEnemy = false;

    [SerializeField] private int lifes;
    private Vector3 InitialPosition;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Instancio el sistema de part?culas al recoger un ?tem y se destruye al cabo de 1sg.
    /// Si se cambia de sentencia, se instancia donde est? el item en lugar de donde est? el jugador.
    /// </summary>
    /// <param name="positionItem">Posici?n del item que se recoge</param>
    public void ShowUFXPickUpItem(Transform positionItem)
    {
        //Destroy(Instantiate(pickUpItemUFX, positionItem.position, positionItem.rotation), 1.0f);
        Destroy(Instantiate(pickUpItemUFX, gameObject.transform), 1.0f);
    }

    /// <summary>
    /// Aplica un impulso al personaje en funci?n de la fuerza 
    /// </summary>
    /// <param name="force">Fuerza que se aplicar? para realizar el impulso</param>
    public void DoImpulse(Vector3 force)
    {
        _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }

    //Esto es por un posible... a?adir vidas al estilo mariobros
   /* public void RestLife()
    {
        lifes -= 1;
        if (lifes == 0)
        {
            Debug.Log("Fin de la partida");
        }
    }*/

    private void Update()
    {
        if (collisioned)
        {
            timeCollisioned += Time.deltaTime;
            if (timeCollisioned >= timeUntilBeCollisioned)
            {
                collisioned = false;
                timeCollisioned = 0.0f;
            }
        }

        if (hitEnemy)
        {
            timeHitEnemy += Time.deltaTime;
            if (timeHitEnemy >= timeUntilHitEnemyAgain)
            {
                hitEnemy = false;
                timeHitEnemy = 0.0f;
            }
        }
    }
}
