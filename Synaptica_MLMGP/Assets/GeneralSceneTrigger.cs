using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.Rendering;
using UnityEngine.Video;
using UnityEngine.Playables;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class GeneralSceneTrigger : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip firstClip;
    [SerializeField] private AudioClip secondClip;
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<GameObject> robots;
    [SerializeField] private ExplosiveInteractable explosive;
    private BoxCollider[] colliders;
    private List<BlazeAI> blazeAIs = new();
    private bool played;
    private bool entered;

    private void Start()
    {
        played = false;
        entered = false;
        BoxCollider[] allColliders = GetComponents<BoxCollider>();
        colliders = new BoxCollider[allColliders.Length - 1];
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i] = GetComponents<BoxCollider>()[i + 1];
        }
        foreach (GameObject robot in robots)
        {
            blazeAIs.Add(robot.GetComponent<BlazeAI>());
        }
    }

    private void Update()
    {
        if (!entered || played) return;

        if (explosive == null)
        {
            StartCoroutine(Begin());
            played = true;
        }
        else if (explosive.planted)
        {
            StartCoroutine(Begin());
            played = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) entered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) entered = false;
    }

    private void ToggleColliders()
    {
        foreach (BoxCollider collider in colliders) collider.enabled = !collider.enabled;
    }

    private IEnumerator Begin()
    {
        AudioSource source = SoundManager.Instance.GetMusicSource();
        ToggleColliders();
        videoPlayer?.Play();
        playerMovement?.LookAt(videoPlayer.transform, 1.5f);

        yield return new WaitForSeconds(1);

        if (firstClip != null)
        {
            SoundManager.Instance.PlayMusicFadeIn(firstClip, 0.2f, 0.2f, false);
            while (source.isPlaying)
            {
                yield return null;
            }
            if (secondClip != null) SoundManager.Instance.PlayMusicFadeIn(secondClip, 0.2f, 0, true);
        }
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        playerMovement?.StopLookingAt();
        playerStats.CheckIfCanGrantControl();
        foreach (BlazeAI ai in blazeAIs)
        {
            ai.enabled = true;
        }
        ToggleColliders();
        yield return null;
    }
}
