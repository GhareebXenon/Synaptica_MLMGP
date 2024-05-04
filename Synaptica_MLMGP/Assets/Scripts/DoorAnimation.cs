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
    [SerializeField] private Animator animator;
    [SerializeField] private bool isLocked = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isLocked)
        {
            interactText = "Locked";
        }
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
        if (!isLocked)
        {
            animator.SetBool("IsOpen", true);
            interactText = "close";
        }
    }

    public void Close()
    {
        animator.SetBool("IsOpen", false);
        interactText = "open";
    }

    public void UnLock() => isLocked = false;
}
