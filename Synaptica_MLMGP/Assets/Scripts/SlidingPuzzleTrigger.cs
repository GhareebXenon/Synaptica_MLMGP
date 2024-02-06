using cowsins;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject slidingPuzzleCamera;
    [SerializeField] private WeaponController weaponController;
    public bool enteredTrigger = false;
    


    private void Start()
    {
        slidingPuzzleCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.SetActive(false);
            slidingPuzzleCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //stats.LoseControl();
            playerUI.SetActive(false);
            enteredTrigger = true;
            weaponController.canShoot = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.SetActive(true);
            slidingPuzzleCamera.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //stats.CheckIfCanGrantControl();
            playerUI.SetActive(true);
            enteredTrigger = false;
            weaponController.canShoot = true;
        }
    }

    
}
