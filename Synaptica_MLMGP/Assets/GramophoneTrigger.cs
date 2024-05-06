using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramophoneTrigger : MonoBehaviour
{
    private AudioSource gramophone;

    private void Awake()
    {
        gramophone = transform.parent.Find("Audio").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gramophone.Stop();
        }
    }
}
