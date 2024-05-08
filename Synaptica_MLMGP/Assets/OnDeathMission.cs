using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathMission : MonoBehaviour
{
    [SerializeField] string mission;
    [SerializeField] AudioClip musicAfterDeath;

    public void UpdateMission()
    {
        MissionManager.Instance.UpdateMission(mission, 1);
        if (musicAfterDeath != null) SoundManager.Instance.PlayMusicFadeIn(musicAfterDeath, 0.25f, 0.6f);
        else Debug.LogWarning($"Cannot find mission '{mission}'");

    }
}
