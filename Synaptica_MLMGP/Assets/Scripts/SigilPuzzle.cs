using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SigilPuzzle : MonoBehaviour
{
    [SerializeField] private string mission;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject sigilPuzzleCamera;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private GameObject[] connectingPoints;
    [SerializeField] private List<SigilLine> solutionLines = new();
    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material unselectMaterial;
    [SerializeField] private Color accessGrantedColor;
    [SerializeField] private Color accessDeniedColor;
    [SerializeField] private GameObject selectedPoint;
    [SerializeField] private UnityEvent onAccessGranted;
    [SerializeField] private UnityEvent onAccessDenied;
    [SerializeField] private List<SigilLine> solvedLines = new();
    private Camera camera;
    private GameObject board;
    private List<GameObject> drawnPoints = new();
    private List<GameObject> drawnPointsOld = new();

    private void Start()
    {
        camera = sigilPuzzleCamera.GetComponent<Camera>();
        board = transform.Find("Board").gameObject;
        board.GetComponent<Light>().enabled = false;
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
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

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
    }

    private void DrawLine(SigilLine line)
    {
        Vector3 offset = new Vector3(0, 0, connectingPoints[0].transform.localPosition.z + 0.007f);
        Vector3 start = line.startPoint.transform.position + offset;
        Vector3 end = line.endPoint.transform.position + offset;
        Vector3 direction = end - new Vector3(start.x, start.y, 0);
        float distance = direction.magnitude;
        direction.Normalize();

        for (float i = 0; i < distance; i += 0.4f)
        {
            Vector3 currentPosition = start + new Vector3(direction.x * i, direction.y * i, 0);
            GameObject newPoint = Instantiate(bloodPrefab, currentPosition, bloodPrefab.transform.rotation, board.transform);
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
            GrantAccess();
        }
        else
        {
            int pointCount = 0;
            foreach (GameObject point in connectingPoints)
            {
                if (point.name.Contains("*"))
                {
                    pointCount++;
                }
            }
            if (pointCount == connectingPoints.Length)
            {
                DenieAccess();
            }
        }
    }

    private void DenieAccess()
    {
        onAccessDenied?.Invoke();
        ResetPoints();
        StartCoroutine(ChangeColor(drawnPointsOld, accessDeniedColor));
    }

    private void GrantAccess()
    {
        StartCoroutine(ChangeColor(drawnPoints, accessGrantedColor));
        StartCoroutine(LightsOn());
    }

    private void ResetPoints()
    {
        foreach (GameObject point in connectingPoints)
        {
            if (point.name.Contains("*"))
            {
                point.name = point.name.Replace("*", "");
            }
            point.GetComponent<MeshRenderer>().material = unselectMaterial;
        }
        foreach (GameObject point in drawnPoints)
        {
            drawnPointsOld.Add(point);
        }
        drawnPoints.Clear();
        solvedLines.Clear();
        selectedPoint = null;
    }

    IEnumerator ChangeColor(List<GameObject> points, Color newColor, float transitionTime = 0.5f)
    {
        Color currColor = points[0].GetComponent<Renderer>().material.color;
        float elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            currColor = Color.Lerp(currColor, newColor, elapsedTime / 2);
            foreach (GameObject point in points)
            {
                point.GetComponent<Renderer>().material.color = currColor;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject point in points)
        {
            point.GetComponent<Renderer>().material.color = newColor;
        }
    }

    IEnumerator LightsOn(float transitionTime = 0.5f)
    {
        float elapsedTime = 0f;
        board.GetComponent<Light>().enabled = true;
        while (elapsedTime < transitionTime)
        {
            board.GetComponent<Light>().intensity = Mathf.Lerp(0, 50, elapsedTime / 2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        board.GetComponent<Light>().intensity = 50;
        onAccessGranted?.Invoke();
        MissionManager.Instance.UpdateMission(mission, 1);
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp(float transitionTime = 5f)
    {
        float elapsedTime = 0f;
        yield return new WaitForSeconds(0.5f);
        while (elapsedTime < transitionTime)
        {
            float newHeight = Mathf.Lerp(transform.localPosition.y, 8, elapsedTime / transitionTime);
            transform.localPosition = new Vector3(transform.localPosition.x, newHeight, transform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, 8, transform.localPosition.z);
    }
}
