using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeDamage : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats player = other.GetComponent<PlayerStats>();
        player.Damage(20);
       

    }
}
