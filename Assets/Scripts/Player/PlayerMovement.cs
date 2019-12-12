using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController2D controller;
    // Greift auf den Animator zu
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 

    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    
}
