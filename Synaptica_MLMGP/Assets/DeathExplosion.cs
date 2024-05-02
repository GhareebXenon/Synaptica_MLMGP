using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplosion : MonoBehaviour
{
    
    [SerializeField] private GameObject explosionVFX;
    public  void StartExplosion()
    {
       
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosionVFX, 3f);
    }
    
}
