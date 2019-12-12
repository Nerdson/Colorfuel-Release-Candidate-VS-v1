using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxPlayerHealth;

    public static int playerHealth;

    // Text text;
    public Slider healthBar;

    private LevelManager levelManager;

    public bool isDead;

    public AudioSource playerDeathSounds;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // text = GetComponent<Text>();
        healthBar = GetComponent<Slider>();

        playerHealth = maxPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0 && !isDead)
        {
            playerDeathSounds.Play();
            playerHealth = 0;
            levelManager.RespawnPlayer();
            isDead = true;
            animator.SetTrigger("isDead");
        }

        if (playerHealth > maxPlayerHealth)
            playerHealth = maxPlayerHealth;

        // text.text = "" + playerHealth;
        healthBar.value = playerHealth;
    }

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
        PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }
}
