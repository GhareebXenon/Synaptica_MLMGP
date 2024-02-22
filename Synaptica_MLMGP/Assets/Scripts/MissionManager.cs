using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<Mission> missions = new List<Mission>();
    [SerializeField] private GameObject missionUI;
    [SerializeField] private GameObject missionPrefap;

    private int notMissions;

    private void Start()
    {
        notMissions = missionUI.transform.childCount;
        RefreshMissions();
    }

    private void RefreshMissions()
    {
        int missionCount = missionUI.transform.childCount - notMissions;

        for (int i = 0; i < missions.Count; i++)
        {
            Transform missionCheck = missionUI.transform.Find($"{missions[i].title}");
            if (missions[i] != null)
            {
                if (missions[i].isActive && missionCheck == null)
                {
                    int height = 0 - missionCount * 50;
                    missionCount++;
                    GameObject instance = Instantiate(missionPrefap, missionUI.transform);
                    instance.name = $"{missions[i].title}";
                    instance.transform.Find("MissionText").GetComponent<TextMeshProUGUI>().text = missions[i].title;
                    instance.transform.localPosition = new Vector3(0, height, 0);
                    foreach (SubMission subMission in missions[i].subMissions)
                    {
                        Transform subMissionCheck = missionUI.transform.Find($"{subMission.title}");
                        if (subMission.isActive && subMissionCheck == null)
                        {
                            int subHeight = 0 - missionCount * 50;
                            missionCount++;
                            GameObject subInstance = Instantiate(missionPrefap, missionUI.transform);
                            subInstance.name = $"{subMission.title}";
                            subInstance.transform.Find("MissionText").GetComponent<TextMeshProUGUI>().text = subMission.title;
                            subInstance.transform.Find("MissionText").GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                            subInstance.transform.Find("MissionText").GetComponent<TextMeshProUGUI>().fontSize *= 0.85f;
                            subInstance.transform.localPosition = new Vector3(0, subHeight, 0);
                        }
                        else if (subMission.isActive == false && subMissionCheck != null)
                        {
                            Destroy(subMissionCheck.gameObject);
                        }
                    }
                }
                else if (missions[i].isActive && missionCheck != null)
                {
                    foreach (SubMission subMission in missions[i].subMissions)
                    {
                        Transform subMissionCheck = missionUI.transform.Find($"{subMission.title}");
                        if (subMission.isActive && subMissionCheck == null)
                        {
                            int subHeight = 0 - missionCount * 50;
                            missionCount++;
                            GameObject subInstance = Instantiate(missionPrefap, missionUI.transform);
                            subInstance.name = $"{subMission.title}";
                            subInstance.transform.Find("MissionText").GetComponent<TextMeshProUGUI>().text = subMission.title;
                            subInstance.transform.Find("MissionText").GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                            subInstance.transform.Find("MissionText").GetComponent<TextMeshProUGUI>().fontSize *= 0.85f;
                            subInstance.transform.localPosition = new Vector3(0, subHeight, 0);
                        }
                        else if (subMission.isActive == false && subMissionCheck != null)
                        {
                            Destroy(subMissionCheck.gameObject);
                        }
                    }
                }
                else if (missions[i].isActive == false && missionCheck != null)
                {
                    Destroy(missionCheck.gameObject);
                }
            }
            
        }

        for (int i = 0; i < missions.Count; i++)
        {
            
        }

        CheckCompleted();
    }

    public void UpdateMission(string title, float achieved)
    {
        Mission mission = missions.Find(mission => mission.title.Trim().ToLower() == title.Trim().ToLower());
        SubMission subMission;
        if (mission != null )
        {
            mission.achieved += achieved;
            CheckCompleted();
            return;
        }
        else
        {
            foreach (Mission m in missions)
            {
                subMission = m.subMissions.Find(subMission => subMission.title.Trim().ToLower() == title.Trim().ToLower());
                if (subMission != null)
                {
                    subMission.achieved += achieved;
                    CheckCompleted();
                    return;
                }
            }
        }
    }

    private void CheckCompleted()
    {
        foreach (Mission mission in missions)
        {
            int completion = 0;
            foreach (SubMission subMission in mission.subMissions)
            {
                if (subMission.achieved >= subMission.target) 
                {
                    TextMeshProUGUI missionText = missionUI.transform.Find(subMission.title).Find("MissionText").GetComponent<TextMeshProUGUI>();
                    missionText.text = $" <s>{subMission.title}</s>";
                    missionText.color = new Color(missionText.color.r, missionText.color.g, missionText.color.b, 0.7f);
                    subMission.OnCompleted.Invoke();
                    completion++;
                }
            }
            if (mission.achieved >= mission.target)
            {
                completion++;
            }
            if (completion == mission.subMissions.Count + 1)
            {
                TextMeshProUGUI missionText = missionUI.transform.Find(mission.title).Find("MissionText").GetComponent<TextMeshProUGUI>();
                missionText.text = $"<s>{mission.title}</s>";
                missionText.color = new Color(missionText.color.r, missionText.color.g, missionText.color.b, 0.7f);
                mission.OnCompleted.Invoke();
            }
        }
    }

    public void AddMission(Mission mission)
    {
        if (missions.Contains(mission))
        {
            return;
        }
        else
        {
            if (mission != null)
            {
                missions.Add(mission);
                RefreshMissions();
            }
        }
    }

    public void AddSubMission(SubMission mission, string mainMissionTitle)
    {
        Mission mainMission = missions.Find(mainMission => mainMission.title.Trim().ToLower() == mainMissionTitle.Trim().ToLower());
        if (mainMission != null)
        {
            if (mainMission.subMissions.Contains(mission))
            {
                return;
            }
            else
            {
                if (mission != null)
                {
                    mainMission.subMissions.Add(mission);
                    RefreshMissions();
                }
            }
        }
    }
}
