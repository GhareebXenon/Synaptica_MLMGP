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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip lockedSfx;
    [SerializeField] private bool isLocked = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        if (animator.GetBool("IsOpen") == false && !isLocked)
        {
            Open();
        }
        else if (animator.GetBool("IsOpen") == true)
        {
            Close();
        }
        else
        {
            if (!audioSource.isPlaying) audioSource.PlayOneShot(lockedSfx);
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

    public void UnLock() => isLocked = false;
}
