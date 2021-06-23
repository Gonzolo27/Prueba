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

    public void MakeDamage()
    {
        currentHealth -= 1;
        sliderHealth.value -= 1;
        if (currentHealth <= 0)
        {
            //playerManager.RestLife();
            FindObjectOfType<UIManager>().Dead();
        }
    }
}
