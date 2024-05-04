using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ObeliskPuzzle : MonoBehaviour
{
    [SerializeField] private string mission;
    [SerializeField] private UnityEvent OnCompleted;
    [SerializeField] private bool completed = false;
    [SerializeField] private Transform[] cubes;
    [SerializeField] private ObeliskRunes[] runes;
    [SerializeField] private ObeliskRunes operationRune;
    [SerializeField] private ObeliskRunes solutionRune;
    public List<int> runesNumbers;
    [SerializeField] private int[] runesSelected;
    [SerializeField] private int solution;
    [SerializeField] private string operation;

    private void Start()
    {
        runesSelected = new int[3];

        foreach (ObeliskRunes rune in runes)
        {
            rune.gameObject.GetComponent<MeshRenderer>().material.mainTexture = rune.runeSprite;
            rune.gameObject.name += $" {rune.runeSprite.name}";
            if (int.TryParse(rune.runeSprite.name.Trim(), out int runeNumber))
            {
                runesNumbers.Add(runeNumber);
            }
        }

        operationRune.gameObject.GetComponent<MeshRenderer>().material.mainTexture = operationRune.runeSprite;
        operation = operationRune.runeSprite.name;

        solutionRune.gameObject.GetComponent<MeshRenderer>().material.mainTexture = solutionRune.runeSprite;
        int.TryParse(solutionRune.runeSprite.name.Trim(), out solution);

        UpdateSelected();
    }

    private void Update()
    {
        if (completed)
        {
            OnCompleted?.Invoke();
            MissionManager.Instance.UpdateMission(mission, 1);
            completed = false;
        }
    }

    public void UpdateSelected()
    {
        for (int i = 0; i < 3; i++)
        {
            int rotation = (int)cubes[i].localEulerAngles.y;
            if (rotation == -90)
            {
                runesSelected[i] = runesNumbers[(i * 4) + 3];
            }
            else
            {
                runesSelected[i] = runesNumbers[(Mathf.Abs(rotation) / 90) + (i * 4)];
            }
            Debug.LogWarning(cubes[i].name + "Rotation = " + rotation);
        }
        CheckComplete();
    }

    public void CheckComplete()
    {
        int result;
        if (operation == "+")
        {
            result = runesSelected[0] + runesSelected[1] + runesSelected[2];
            if (result == solution)
            {
                completed = true;
            }
            else
            {
                completed = false;
            }
            Debug.Log($"{runesSelected[0]} + {runesSelected[1]} + {runesSelected[2]} = {result}, Completed? {completed}");
        }
        else if (operation == "-")
        {
            result = runesSelected[0] - runesSelected[1] - runesSelected[2];
            if (result == solution)
            {
                completed = true;
            }
            else
            {
                completed = false;
            }
            Debug.Log($"{runesSelected[0]} - {runesSelected[1]} - {runesSelected[2]} = {result}, Completed? {completed}");
        }
        else if (operation == "×")
        {
            result = runesSelected[0] * runesSelected[1] * runesSelected[2];
            if (result == solution)
            {
                completed = true;
            }
            else
            {
                completed = false;
            }
            Debug.Log($"{runesSelected[0]} * {runesSelected[1]} * {runesSelected[2]} = {result}, Completed? {completed}");
        }
        else if (operation == "÷")
        {
            result = runesSelected[0] / runesSelected[1] / runesSelected[2];
            if (result == solution)
            {
                completed = true;
            }
            else
            {
                completed = false;
            }
            Debug.Log($"{runesSelected[0]} / {runesSelected[1]} / {runesSelected[2]} = {result}, Completed? {completed}");
        }
    }
}
