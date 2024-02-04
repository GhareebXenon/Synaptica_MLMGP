using cowsins;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadTrigger : MonoBehaviour
{
    public GameObject playerUI;
    //public PlayerStats stats;
    public GameObject mainCamera;
    public GameObject keypadCamera;

    private void Start()
    {
        keypadCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.SetActive(false);
            keypadCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //stats.LoseControl();
            playerUI.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.SetActive(true);
            keypadCamera.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //stats.CheckIfCanGrantControl();
            playerUI.SetActive(true);

        }
    }

    
}
