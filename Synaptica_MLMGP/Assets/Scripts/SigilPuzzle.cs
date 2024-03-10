using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SigilPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject[] connectingPoints;
    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material unselectMaterial;
    [SerializeField] private GameObject selectedPoint;
    private Camera sigilPuzzleCamera;
    private List<GameObject> drawnPoints = new List<GameObject>();

    private void Start()
    {
        sigilPuzzleCamera = transform.Find("SigilPuzzleCamera").GetComponent<Camera>();
        board = transform.Find("Board").gameObject;
        foreach (var point in connectingPoints)
        {
            point.GetComponent<MeshRenderer>().material = unselectMaterial;
        }
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
                    Vector3 pointPosition = hit.point;

                    if (selectedPoint != null && selectedPoint != hit.collider.gameObject)
                    {
                        Vector3 start = selectedPoint.transform.position + new Vector3(0, 0, 0.001f);
                        Vector3 end = pointPosition + new Vector3(0, 0, 0.001f);
                        DrawLine(start, end);
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
        }
        else if (Input.GetMouseButtonUp(0))
        {
            drawnPoints.Clear();
        }
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - new Vector3(start.x, start.y, 0);
        float distance = direction.magnitude;
        direction.Normalize();

        for (float i = 0; i < distance; i += 0.025f)
        {
            Vector3 currentPosition = start + new Vector3(direction.x * i, direction.y * i, 0);
            GameObject newPoint = Instantiate(pointPrefab, currentPosition, pointPrefab.transform.rotation, transform);
            drawnPoints.Add(newPoint);
        }
    }
}
