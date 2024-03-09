using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilPuzzleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject playerUIContainer;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject sigilPuzzleCamera;
    [SerializeField] private WeaponController weaponController;
    private SigilPuzzle sigilPuzzle;
    public bool enteredTrigger = false;



    private void Start()
    {
        sigilPuzzle = transform.parent.GetComponent<SigilPuzzle>();
        sigilPuzzleCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.SetActive(false);
            sigilPuzzleCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //stats.LoseControl();
            playerUIContainer.SetActive(false);
            enteredTrigger = true;
            weaponController.canShoot = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.SetActive(true);
            sigilPuzzleCamera.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //stats.CheckIfCanGrantControl();
            playerUIContainer.SetActive(true);
            enteredTrigger = false;
            weaponController.canShoot = true;
        }
    }
}
