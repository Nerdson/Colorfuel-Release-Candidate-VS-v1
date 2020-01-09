using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxPlayerHealth;

    public static int playerHealth;

    // Text text;
    

    private LevelManager levelManager;

    public bool isDead;

    public AudioSource playerDeathSounds;

    private Animator animator;



    public Sprite[] HeartSprites;

    public Image HeartUI;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // text = GetComponent<Text>();
        

        playerHealth = maxPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0 && !isDead)
        {
            animator.SetTrigger("isDead");
            playerDeathSounds.Play();
            playerHealth = 0;
            StartCoroutine(DeathAnim());
            levelManager.RespawnPlayer();
            isDead = true;  
        }

        if (playerHealth > maxPlayerHealth)
            playerHealth = maxPlayerHealth;

        HeartUI.sprite = HeartSprites[playerHealth];
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

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(3f);
    }
}
