using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour
{
    public Animator animator;

    public int ComboCount;
    public bool CanCombo;
    void Start()
    {
        animator = GetComponent<Animator>();
        ComboCount = 0;
        CanCombo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ComboStart();
        }
    }

    void ComboStart()
    {
        if (CanCombo)
        {
            ComboCount++;
        }

        if(ComboCount == 1)
        {
            animator.SetInteger("attack", 1);
        }
    }

    public void VerifyCombo()
    {
        CanCombo = false;

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit1") && ComboCount == 1)
        {
            animator.SetInteger("attack", 0);
            CanCombo = true;
            ComboCount = 0;
        } 
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit1") && ComboCount >= 2)
        {
            animator.SetInteger("attack", 2);
            CanCombo = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit2") && ComboCount == 2)
        {
            animator.SetInteger("attack", 0);
            CanCombo = true;
            ComboCount = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit2") && ComboCount >= 3)
        { 
            animator.SetInteger("attack", 3);
            CanCombo = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))
        {
            animator.SetInteger("attack", 0);
            CanCombo = true;
            ComboCount = 0;
        }
    }
}
