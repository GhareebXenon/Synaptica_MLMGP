using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gramophone : MonoBehaviour
{
    private AudioSource azazelTheSinger;

    private AudioSource gramophone;

    private void Awake()
    {
        azazelTheSinger = transform.Find("AzazelTheSinger").GetComponent<AudioSource>();
        gramophone = GetComponent<AudioSource>();
        gramophone.Play();
        azazelTheSinger.Play();
    }
}
