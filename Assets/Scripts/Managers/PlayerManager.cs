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


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Instancio el sistema de partículas al recoger un ítem y se destruye al cabo de 1sg.
    /// Si se cambia de sentencia, se instancia donde está el item en lugar de donde esté el jugador.
    /// </summary>
    /// <param name="positionItem"></param>
    public void ShowUFXPickUpItem(Transform positionItem)
    {
        //Destroy(Instantiate(pickUpItemUFX, positionItem.position, positionItem.rotation), 1.0f);
        Destroy(Instantiate(pickUpItemUFX, gameObject.transform), 1.0f);
    }

    public void DoImpulse(Vector3 force)
    {
        _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }

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
