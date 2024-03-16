using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SigilPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject[] connectingPoints;
    [SerializeField] private List<SigilLine> solutionLines = new();
    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material unselectMaterial;
    [SerializeField] private GameObject selectedPoint;
    [SerializeField] private UnityEvent OnCompleted;
    private Camera sigilPuzzleCamera;
    [SerializeField] private List<SigilLine> solvedLines = new();
    private List<GameObject> drawnPoints = new();

    private void Start()
    {
        sigilPuzzleCamera = transform.Find("SigilPuzzleCamera").GetComponent<Camera>();
        board = transform.Find("Board").gameObject;
        foreach (var point in connectingPoints)
        {
            point.GetComponent<MeshRenderer>().material = unselectMaterial;
        }

        List<SigilLine> addedLines = new();
        for (int i = 0; i < solutionLines.Count; i++)
        {
            SigilLine line = solutionLines[i];
            SigilLine invertedLine = new(line.endPoint, line.startPoint);
            addedLines.Add(invertedLine);
            Debug.Log($"Added line from {line.endPoint} to {line.startPoint}.");
        }
        solutionLines.AddRange(addedLines);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = sigilPuzzleCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("ConnectingPoint"))
                {
                    if (selectedPoint != null && selectedPoint != hit.collider.gameObject)
                    {
                        SigilLine line = new SigilLine(selectedPoint, hit.collider.gameObject);
                        DrawLine(line);
                        hit.collider.GetComponent<MeshRenderer>().material = selectMaterial;
                        hit.collider.gameObject.name += "*";
                        selectedPoint.name += "*";
                        selectedPoint = null;
                    }
                    else
                    {
                        if (!hit.collider.gameObject.name.Contains("*"))
                        {
                            selectedPoint = hit.collider.gameObject;
                            selectedPoint.GetComponent<MeshRenderer>().material = selectMaterial;
                        }
                    }
                }
                else
                {
                    if (selectedPoint != null)
                    {
                        selectedPoint.GetComponent<MeshRenderer>().material = unselectMaterial;
                        selectedPoint = null;
                    }
                }
            }

            CheckCompleted();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            drawnPoints.Clear();
        }
    }

    private void DrawLine(SigilLine line)
    {
        Vector3 start = line.startPoint.transform.position + new Vector3(0, 0, 0.001f);
        Vector3 end = line.endPoint.transform.position + new Vector3(0, 0, 0.001f);
        Vector3 direction = end - new Vector3(start.x, start.y, 0);
        float distance = direction.magnitude;
        direction.Normalize();

        for (float i = 0; i < distance; i += 0.025f)
        {
            Vector3 currentPosition = start + new Vector3(direction.x * i, direction.y * i, 0);
            GameObject newPoint = Instantiate(pointPrefab, currentPosition, pointPrefab.transform.rotation, transform);
            drawnPoints.Add(newPoint);
        }
        solvedLines.Add(line);
    }

    private void CheckCompleted()
    {
        int count = 0;

        for (int i = 0; i < solvedLines.Count; i++)
        {
            for (int j = 0; j < solutionLines.Count; j++)
            {
                if (solvedLines[i].startPoint == solutionLines[j].startPoint && solvedLines[i].endPoint == solutionLines[j].endPoint)
                {
                    count++;
                    Debug.Log($"The line from {solutionLines[i].startPoint.name} to {solutionLines[i].endPoint.name} is correct, Count is {count}.");
                    break;
                }
                else
                {
                    Debug.Log($"The line from {solutionLines[i].startPoint.name} to {solutionLines[i].endPoint.name} is incorrect, Count is still {count}.");
                }
            }
        }

        if (count == solutionLines.Count / 2)
        {
            Debug.Log($"Solved, Count = {count}");
            OnCompleted?.Invoke();
        }
    }
}
