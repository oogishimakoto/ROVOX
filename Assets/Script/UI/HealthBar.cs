using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public int maxHealth;
    public int currentHealth;

    private float lerpSpeed = 0.05f;

    public PlayerInfo playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        if (playerInfo != null)
        {
            healthSlider.maxValue = maxHealth;
            easeHealthSlider.maxValue = maxHealth;
        }
        else
        {
            Debug.LogError("PlayerInfo script is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerInfo.HP;


        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed);
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
         */
    }

    private void TakeDamage(int damage)
    {
        playerInfo.HP -= damage;
    }
}
