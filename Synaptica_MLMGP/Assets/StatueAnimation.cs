using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class StatueAnimation : MonoBehaviour
{
    [SerializeField] private AudioClip sfx;
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.85f, 1.05f);
    }

    public void PlayAnimation()
    {
        animator.SetBool("isPlaying", true);
        if (sfx != null)
        {
            audioSource.PlayOneShot(sfx);
        }
    }
}
