using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.Events;

public class DoorActions : MonoBehaviour
{
    private PlayerActions input = null;
    [SerializeField]
    private TextMeshPro UseText;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask InteractLayer;


    private void Awake()
    {
        input = new PlayerActions();
    }
    private void OnEnable()
    {
        input.Enable();
        input.GameControls.Interacting.performed += Interacting_performed;
    }

    private void Interacting_performed(InputAction.CallbackContext obj)
    {
        PerformInteraction();
        
        
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void PerformInteraction()
    { 
        if(Physics.Raycast(Camera.position, Camera.forward,out RaycastHit hit, MaxUseDistance, InteractLayer))
        {
            if(hit.collider.TryGetComponent<SciFiDoor>(out SciFiDoor door))
            {
                if(door.isOpen)
                {
                    door.Close();
                   
                }
                else
                {
                    door.Open();
                 
                }
            
            }
        }
    }
    private void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, InteractLayer)
            && (hit.collider.TryGetComponent<SciFiDoor>(out SciFiDoor door)))
        { 
            if(door.isOpen) {
                UseText.SetText("Close \"E\"");
            }else
            {
                UseText.SetText("Open \"E\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point-(hit.point -Camera.position).normalized *0.5f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
       
             
    }

}
