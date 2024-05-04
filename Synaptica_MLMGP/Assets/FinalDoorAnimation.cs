using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.SetBool("IsOpen", true);
    }
}
