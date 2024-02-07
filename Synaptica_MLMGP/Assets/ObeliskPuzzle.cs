using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ObeliskPuzzle : MonoBehaviour
{
    
    [SerializeField] private Transform[] cubes;
    [SerializeField] private UnityEvent OnCompleted;
    [SerializeField] private bool completed = false;
    public List<int> runesNumbers;
    [SerializeField] private int[] runesSelected;
    [SerializeField] private int solution;
    public int operation = 0;
    

    private void Start()
    {
        runesSelected = new int[3];
    }

    private void Update()
    {
        if (completed)
        {
            OnCompleted?.Invoke();
            completed = false;
        }
    }

    public void UpdateSelected()
    {
        for (int i = 0; i < 3; i++)
        {
            int rotation = (int)cubes[i].eulerAngles.y;
            if (rotation == -90)
            {
                runesSelected[i] = runesNumbers[(i * 4) + 4];
            }
            else
            {
                runesSelected[i] = runesNumbers[(Mathf.Abs(rotation) / 90) + (i * 4) + 1];
            }
        }
        solution = runesNumbers[0];
        CheckComplete();
    }

    public void CheckComplete()
    {
        int result = 0;

        if (operation == 0)
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
        else if (operation == 1)
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
        else if (operation == 2)
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
        else if (operation == 3)
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
