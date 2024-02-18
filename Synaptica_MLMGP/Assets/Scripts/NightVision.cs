using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVision : MonoBehaviour
{
    [SerializeField] GameObject nightVision;
    private void Update()
    {
        //Desiable\enable the NightVision with N Key
        if(Input.GetKeyDown(KeyCode.N))
            nightVision.SetActive(!nightVision.activeInHierarchy);
    }
}
