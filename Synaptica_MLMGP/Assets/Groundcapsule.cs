using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergeOnInteract : MonoBehaviour
{
    [SerializeField] private float riseDistance; 
    [SerializeField] private float riseDuration;
    [SerializeField] private string playerTag; 

    private Vector3 initialPosition; 

    private void Start()
    {
        
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(playerTag))
        {
           
            StartCoroutine(RiseFromGround());
        }
    }

    IEnumerator RiseFromGround()
    {
        float elapsedTime = 0f;

      
        while (elapsedTime < riseDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * riseDistance, elapsedTime / riseDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}