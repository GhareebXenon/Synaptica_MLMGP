using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.Rendering;

public class MissionTrigger : MonoBehaviour
{
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private string mission;
    [SerializeField] private float acheived = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            missionManager.UpdateMission(mission, acheived);
        }
    }
}
