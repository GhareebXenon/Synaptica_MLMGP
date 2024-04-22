using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MusicTrigger : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip musicOld;
    [SerializeField][Tooltip("Plays when the player enter the trigger.")] private AudioClip musicNew;
    [SerializeField][Tooltip("Plays after Music New, leave it Null if playing one track.")] private AudioClip musicAfter;

    [Header("Attributes for Music New")]
    [SerializeField] private float transitionTime = 1.5f;
    [SerializeField] private float volume = 0.25f;
    [SerializeField] private bool loopable = true;

    [Header("Attributes for Music After")]
    [SerializeField] private float transitionTimeAfter = 1.5f;
    [SerializeField] private float volumeAfter = 0.25f;
    [SerializeField] private bool loopableAfter = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioClip musicCurr = SoundManager.Instance.GetMusicSource()?.clip; //Get current music playing in audio source
            Debug.LogWarning("Player entered the music trigger.");

            if (musicNew != null && musicNew != musicCurr) //Check if musicNew is not null and not playing
            {
                if (musicAfter == null) //if we didn't put a reference for musicAfter just play musicNew
                {
                    SoundManager.Instance.PlayMusicFadeIn(musicNew, volume, transitionTime, loopable);
                    Debug.LogWarning($"Music changed to '{musicNew.name}'.");
                }
                else //else if we put a reference for musicAfter start the coroutine that plays it after musicNew
                {
                    StartCoroutine(PlayMusicAfter());
                }
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.LogWarning("Player exited the music trigger.");

    //        if (musicOld != null)
    //        {
    //            SoundManager.Instance.PlayMusicFadeIn(musicOld, 0.25f);
    //            Debug.LogWarning($"Music changed to '{musicOld.name}'.");
    //        }
    //    }
    //}

    IEnumerator PlayMusicAfter()
    {
        yield return null;

        AudioSource source = SoundManager.Instance.GetMusicSource();

        SoundManager.Instance.PlayMusicFadeIn(musicNew, volume, transitionTime, loopable);
        Debug.LogWarning($"Music changed to '{musicNew.name}'.");

        while (source.isPlaying)
        {
            yield return null;
        }

        SoundManager.Instance.PlayMusicFadeIn(musicAfter, volumeAfter, transitionTimeAfter, loopableAfter);
        Debug.LogWarning($"Music changed to '{musicAfter.name}' after {musicNew.name} has finished.");
    }
}
