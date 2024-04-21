using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip musicOld;
    [SerializeField] private AudioClip musicNew;
    [SerializeField] private float transitionTime = 1.5f;
    [SerializeField] private float volume = 0.25f;
    [SerializeField] private bool loopable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioClip musicCurr = SoundManager.Instance.GetMusicSource()?.clip;
            Debug.LogWarning("Player entered the music trigger.");

            if (musicNew != null && musicNew != musicCurr)
            {
                SoundManager.Instance.PlayMusicFadeIn(musicNew, volume, transitionTime, loopable);
                Debug.LogWarning($"Music changed to '{musicNew.name}'.");
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
}
