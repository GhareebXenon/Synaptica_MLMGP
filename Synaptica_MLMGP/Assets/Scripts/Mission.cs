using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Mission
{
    public string title;
    public float target;
    public float achieved;
    public UnityEvent OnCompleted;
    public List<SubMission> subMissions = new List<SubMission>();

    public Mission(string title, float target)
    {
        this.title = title;
        this.target = target;
    }
}

[System.Serializable]
public class SubMission
{
    public string title;
    public float target;
    public float achieved;
    public bool isActive;
    public UnityEvent OnCompleted;

    public SubMission(string title, float target, bool isActive)
    {
        this.title = title;
        this.target = target;
        this.isActive = isActive;
    }
}
