using BlazeAISpace;
using cowsins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    [SerializeField] private List<Mission> missions = new List<Mission>();
    [SerializeField] private GameObject missionUI;
    [SerializeField] private GameObject missionPrefap;

    private Mission activeMission;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        RefreshMissions();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        missionUI = GameObject.Find("MissionUI");
        switch (scene.name)
        {
            case "Exposition":
                SetInitialMission("Enter the BIO-NEXUS building");
                break;
            case "Level 1":
                SetInitialMission("Get to the generator room");
                break;
            case "Level 2":
                SetInitialMission("Plant the explosive  ");
                break;
            case "Level 3":
                SetInitialMission("Explore the area");
                break;
            case "Level 4":
                SetInitialMission("Find a way out of the maze");
                break;
            default:
                break;
        }
    }

    private void RefreshMissions()
    {
        if (activeMission != null)
        {
            Transform missionCheck = missionUI?.transform.Find(activeMission.title);
            if (missionCheck == null)
            {
                GameObject instance = Instantiate(missionPrefap, missionUI?.transform);
                TextMeshProUGUI instanceText = instance.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI instanceValue = instance.transform.Find("Value").GetComponent<TextMeshProUGUI>();
                instance.name = activeMission.title;
                instanceText.text = activeMission.title;
                if (activeMission.target > 1)
                {
                    instanceValue.text = $"{activeMission.achieved}/{activeMission.target}";
                }
                else
                {
                    instanceText.margin = new Vector4(-50, 0, -185, 0);
                    instanceValue.gameObject.SetActive(false);
                }
                int subMissionCount = 1;
                foreach (SubMission subMission in activeMission.subMissions)
                {
                    Transform subMissionCheck = missionUI?.transform.Find(subMission.title);
                    if (subMission.isActive && subMissionCheck == null)
                    {
                        int subHeight = 0 - subMissionCount * 45;
                        subMissionCount++;
                        GameObject subInstance = Instantiate(missionPrefap, missionUI?.transform);
                        TextMeshProUGUI subInstanceText = subInstance.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                        TextMeshProUGUI subInstanceValue = subInstance.transform.Find("Value").GetComponent<TextMeshProUGUI>();
                        subInstance.name = subMission.title;
                        subInstanceText.text = subMission.title;
                        subInstanceText.fontStyle = FontStyles.Normal;
                        subInstanceText.fontSize *= 0.85f;
                        subInstance.transform.localPosition = new Vector3(0, subHeight, 0);
                        if (subMission.target > 1)
                        {
                            subInstanceValue.text = $"{subMission.achieved}/{subMission.target}";
                        }
                        else
                        {
                            subInstanceText.margin = new Vector4(-50, 0, -185, 0);
                            subInstanceValue.gameObject.SetActive(false);
                        }
                    }
                    else if (subMission.isActive && subMissionCheck != null)
                    {
                        TextMeshProUGUI subInstanceText = subMissionCheck.Find("Text").GetComponent<TextMeshProUGUI>();
                        TextMeshProUGUI subInstanceValue = subMissionCheck.Find("Value").GetComponent<TextMeshProUGUI>();
                        if (subMission.target > 1)
                        {
                            subInstanceValue.text = $"{subMission.achieved}/{subMission.target}";
                        }
                        else
                        {
                            subInstanceText.margin = new Vector4(-50, 0, -185, 0);
                            subInstanceValue.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        Destroy(subMissionCheck.gameObject);
                    }
                }
            }
            else if (missionCheck != null)
            {
                if (activeMission.target > 1)
                {
                    missionCheck.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = $"{activeMission.achieved}/{activeMission.target}";
                }
                else
                {
                    missionCheck.transform.Find("Text").GetComponent<TextMeshProUGUI>().margin = new Vector4(-50, 0, -185, 0);
                    missionCheck.transform.Find("Value").gameObject.SetActive(false);
                }
                int subMissionCount = 1;
                foreach (SubMission subMission in activeMission.subMissions)
                {
                    Transform subMissionCheck = missionUI?.transform.Find(subMission.title);
                    if (subMission.isActive && subMissionCheck == null)
                    {
                        int subHeight = 0 - subMissionCount * 45;
                        subMissionCount++;
                        GameObject subInstance = Instantiate(missionPrefap, missionUI?.transform);
                        TextMeshProUGUI subInstanceText = subInstance.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                        TextMeshProUGUI subInstanceValue = subInstance.transform.Find("Value").GetComponent<TextMeshProUGUI>();
                        subInstance.name = subMission.title;
                        subInstanceText.text = subMission.title;
                        subInstanceText.fontStyle = FontStyles.Normal;
                        subInstanceText.fontSize *= 0.85f;
                        subInstance.transform.localPosition = new Vector3(0, subHeight, 0);
                        if (subMission.target > 1)
                        {
                            subInstanceValue.text = $"{activeMission.achieved}/{activeMission.target}";
                        }
                        else
                        {
                            subInstanceText.margin = new Vector4(-50, 0, -185, 0);
                            subInstanceValue.gameObject.SetActive(false);
                        }
                    }
                    else if (subMission.isActive && subMissionCheck != null)
                    {
                        TextMeshProUGUI subInstanceText = subMissionCheck.Find("Text").GetComponent<TextMeshProUGUI>();
                        TextMeshProUGUI subInstanceValue = subMissionCheck.Find("Value").GetComponent<TextMeshProUGUI>();
                        if (subMission.target > 1)
                        {
                            subInstanceValue.text = $"{subMission.achieved}/{subMission.target}";
                        }
                        else
                        {
                            subInstanceText.margin = new Vector4(-50, 0, -185, 0);
                            subInstanceValue.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        Destroy(subMissionCheck.gameObject);
                    }
                }
            }
        }
    }

    public void UpdateMission(string title, float achieved)
    {
        Mission mission = missions.Find(mission => mission.title.Trim().ToLower() == title.Trim().ToLower());
        SubMission subMission;
        if (mission != null )
        {
            mission.achieved = Mathf.Clamp(mission.achieved + achieved, 0, mission.target);
            RefreshMissions();
            CheckCompleted();
        }
        else
        {
            foreach (Mission m in missions)
            {
                subMission = m.subMissions.Find(subMission => subMission.title.Trim().ToLower() == title.Trim().ToLower());
                if (subMission != null)
                {
                    subMission.achieved = Mathf.Clamp(subMission.achieved + achieved, 0, subMission.target);
                    RefreshMissions();
                    CheckCompleted();
                    return;
                }
            }
        }
    }

    private void CheckCompleted()
    {
        if (activeMission == null)
        {
            foreach (Mission mission in missions)
            {
                int completion = 0;
                foreach (SubMission subMission in mission.subMissions)
                {
                    if (subMission.achieved >= subMission.target)
                    {
                        TextMeshProUGUI missionText = missionUI?.transform.Find(subMission.title).Find("Text").GetComponent<TextMeshProUGUI>();
                        missionText.text = $" <s>{subMission.title}</s>";
                        missionText.color = new Color(missionText.color.r, missionText.color.g, missionText.color.b, 0.7f);
                        subMission.OnCompleted?.Invoke();
                        completion++;
                    }
                }
                if (mission.achieved >= mission.target)
                {
                    completion++;
                }
                if (completion == mission.subMissions.Count + 1)
                {
                    mission.OnCompleted?.Invoke();
                }
            }
        }
        else
        {
            int completion = 0;
            if (activeMission.subMissions.Count > 0)
            {
                foreach (SubMission subMission in activeMission.subMissions)
                {
                    if (subMission.achieved >= subMission.target)
                    {
                        TextMeshProUGUI missionText = missionUI?.transform.Find(subMission.title).Find("Text").GetComponent<TextMeshProUGUI>();
                        missionText.text = $" <s>{subMission.title}</s>";
                        missionText.color = new Color(missionText.color.r, missionText.color.g, missionText.color.b, 0.7f);
                        subMission.OnCompleted?.Invoke();
                        completion++;
                    }
                }
                completion++;
            }
            else
            {
                if (activeMission.achieved >= activeMission.target)
                {
                    completion++;
                }
            }
            if (completion == activeMission.subMissions.Count + 1)
            {
                activeMission.OnCompleted?.Invoke();
                Animator animator = missionUI?.transform.Find(activeMission.title).GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("active", false);
                }
                if (missions.IndexOf(activeMission) + 1 < missions.Count)
                {
                    Mission nextMission = missions[missions.IndexOf(activeMission) + 1];
                    if (nextMission != null)
                    {
                        SetActiveMission(nextMission);
                    }
                }
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
                CheckCompleted();
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
                    CheckCompleted();
                }
            }
        }
    }

    public void SetActiveMission(string title)
    {
        Mission mission = missions.Find(mission => mission.title.Trim().ToLower() == title.Trim().ToLower());
        if (mission != null)
        {
            if (activeMission != null)
            {
                RefreshMissions();
                Animator animator = missionUI?.transform.Find(activeMission.title).GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("active", false);
                }
                StartCoroutine(DestroyActiveMission());
            }
            StartCoroutine(ChangeActiveMission(mission));
        }
        else
        {
            StartCoroutine(DestroyActiveMission());
            StartCoroutine(ChangeActiveMission(null));
        }
    }

    public void SetActiveMission(Mission mission)
    {
        if (missions.Contains(mission))
        {
            if (activeMission != null)
            {
                RefreshMissions();
                Animator animator = missionUI?.transform.Find(activeMission.title).GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("active", false);
                }
                StartCoroutine(DestroyActiveMission());
            }
            StartCoroutine(ChangeActiveMission(mission));
        }
        else
        {
            StartCoroutine(DestroyActiveMission());
            StartCoroutine(ChangeActiveMission(null));
        }
    }

    public void SetActiveSubMission(Mission mission, bool isActive)
    {
        SubMission subMission;
        foreach (Mission m in missions)
        {
            subMission = m.subMissions.Find(subMission => subMission.title.Trim().ToLower() == mission.title.Trim().ToLower());
            if (subMission != null)
            {
                subMission.isActive = isActive;
                if (isActive)
                {
                    Debug.Log($"Sub-Mission {subMission.title} set to Active.");
                }
                else
                {
                    Debug.Log($"Sub-Mission {subMission.title} set to Inactive.");
                }
                RefreshMissions();
                CheckCompleted();
                return;
            }
            else
            {
                Debug.Log($"Sub-Mission {subMission.title} not found, cannot activate the mission.");
            }
        }
    }

    private void SetInitialMission(string title)
    {
        Mission mission = missions.Find(mission => mission.title.Trim().ToLower() == title.Trim().ToLower());
        SetActiveMission(mission);
        for (int i = 0; i < missions.IndexOf(mission); i++)
        {
            foreach (SubMission subMission in missions[i].subMissions)
            {
                subMission.achieved = subMission.target;
            }
            missions[i].achieved = missions[i].target;
        }
        for (int i = missions.IndexOf(mission); i < missions.Count - 1; i++)
        {
            foreach (SubMission subMission in missions[i].subMissions)
            {
                subMission.achieved = 0;
            }
            missions[i].achieved = 0;
        }
    }

    public void AddMissionEvent(string title, UnityAction eventHandler)
    {
        Mission mission = missions.Find(mission => mission.title.Trim().ToLower() == title.Trim().ToLower());
        SubMission subMission;
        
        if (mission != null)
        {
            mission.OnCompleted.AddListener(eventHandler);
        }
        else
        {
            foreach (Mission m in missions)
            {
                subMission = m.subMissions.Find(subMission => subMission.title.Trim().ToLower() == title.Trim().ToLower());
                if (subMission != null)
                {
                    subMission.OnCompleted.AddListener(eventHandler);
                    return;
                }
            }
        }
    }

    private IEnumerator DestroyActiveMission()
    {
        yield return new WaitForSeconds(0.34f);
        if (activeMission != null)
        {
            foreach (SubMission subMission in activeMission.subMissions)
            {
                Destroy(missionUI?.transform.Find(subMission.title).gameObject);
            }
            GameObject m = missionUI?.transform.Find(activeMission.title).gameObject;
            if (m != null) Destroy(missionUI?.transform.Find(activeMission.title).gameObject);
        }
    }

    private IEnumerator ChangeActiveMission(Mission mission)
    {
        yield return new WaitForSeconds(0.35f);
        activeMission = mission;
        if (mission == null)
            Debug.Log($"Mission {mission.title} could not be found, activeMission set to null.");
        else
            Debug.Log($"activeMission set to {mission.title}.");
        RefreshMissions();
        CheckCompleted();
    }
}
