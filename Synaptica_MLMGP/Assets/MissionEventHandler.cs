using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MissionEventRegistrar : MonoBehaviour
{
    [SerializeField] private List<MissionEvent> missionEvents = new();

    private void Start()
    {
        RegisterMissionEvents();
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
        RegisterMissionEvents();
    }

    private void RegisterMissionEvents()
    {
        if (MissionManager.Instance != null)
        {
            foreach (MissionEvent e in missionEvents)
                MissionManager.Instance.AddMissionEvent(e.missionTitle, e.missionEvent.Invoke);
        }
    }

    [System.Serializable]
    private class MissionEvent
    {
        public string missionTitle;
        public UnityEvent missionEvent;
    }
}