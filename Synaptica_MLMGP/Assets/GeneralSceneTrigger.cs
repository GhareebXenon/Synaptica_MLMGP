using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.Rendering;
using UnityEngine.Video;
using UnityEngine.Playables;

public class GeneralSceneTrigger : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip firstClip;
    [SerializeField] private AudioClip secondClip;
    [Header("References")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private List<GameObject> robots;
    private PlayableDirector scenePlayer;
    private List<BlazeAI> blazeAIs = new();
    private bool played;

    private void Start()
    {
        played = false;
        scenePlayer = GetComponent<PlayableDirector>();
        foreach (GameObject robot in robots)
        {
            blazeAIs.Add(robot.GetComponent<BlazeAI>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!played)
            {
                StartCoroutine(Timer());
                played = true;
            }
        }
    }

    private IEnumerator Timer()
    {
        AudioSource source = SoundManager.Instance.GetMusicSource();
        SoundManager.Instance.PlayMusicFadeIn(firstClip, 0.2f, 0.2f, false);
        videoPlayer.Play();
        scenePlayer.Play();
        while (source.isPlaying)
        {
            yield return null;
        }
        SoundManager.Instance.PlayMusicFadeIn(secondClip, 0.2f, 0, true);
        foreach(BlazeAI ai in blazeAIs)
        {
            ai.enabled = true;
        }
        yield return null;
    }
}
