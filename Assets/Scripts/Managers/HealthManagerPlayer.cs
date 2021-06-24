using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManagerPlayer : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;
    public Slider sliderHealth;


    private PlayerManager playerManager;
    

    void Start()
    {
        currentHealth = maxHealth;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    /// <summary>
    /// Se le quita vida al personaje y se comprueba si tiene menos de 0 de vida
    /// </summary>
    /// <param name="damage">daño causado al personaje</param>
    public void MakeDamage(int damage)
    {
        currentHealth -= damage;
        sliderHealth.value -= damage;
        if (currentHealth <= 0)
        {
            playerManager.collisioned = false;
            FindObjectOfType<UIManager>().Dead();
        }
    }
}
