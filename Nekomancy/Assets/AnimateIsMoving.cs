using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateIsMoving : MonoBehaviour
{
    private Rigidbody2D parentBody;
    private Animator animator;
    private bool prevState = false;
    private int IsMovingHash;

    // Start is called before the first frame update
    void Start()
    {
        parentBody = this.GetComponentInParent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        IsMovingHash = Animator.StringToHash("IsMoving");
    }

    // Update is called once per frame
    void Update()
    {
        if (parentBody.velocity.sqrMagnitude > 1)
        {
            if (prevState == false)
            {
                prevState = true;
                animator.SetBool(IsMovingHash, prevState);
            }
        }
        else
        {
            if (prevState == true)
            {
                prevState = false;
                animator.SetBool(IsMovingHash, prevState);
            }
        }
    }
}
