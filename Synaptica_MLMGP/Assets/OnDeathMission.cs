using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathMission : MonoBehaviour
{
    [SerializeField] private string mission;
    [SerializeField] private AudioClip musicAfterDeath;
    [SerializeField] private float musicVolume = 1;

    public void UpdateMission()
    {
        MissionManager.Instance.UpdateMission(mission, 1);
        if (musicAfterDeath != null) SoundManager.Instance.PlayMusicFadeIn(musicAfterDeath, musicVolume, 0.6f);
        else Debug.LogWarning($"Cannot find mission '{mission}'");
        Debug.LogWarning("Enemy is dead, Mission updated.");
    }
}
