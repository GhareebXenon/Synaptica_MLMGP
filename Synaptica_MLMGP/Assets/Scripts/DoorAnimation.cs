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
    [SerializeField]private Animation animation;
    private void Start()
    {
        animation = GetComponent<Animation>();
    }
    public override void Interact()
    {
       animation.Play();
    }

}
