using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.Events;
using cowsins;

public class DoorAnimation : Interactable
{
    [SerializeField]private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (animator.GetBool("IsOpen") == false)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Open()
    {
        animator.SetBool("IsOpen", true);
        interactText = "close";
    }

    public void Close()
    {
        animator.SetBool("IsOpen", false);
        interactText = "open";
    }
}
