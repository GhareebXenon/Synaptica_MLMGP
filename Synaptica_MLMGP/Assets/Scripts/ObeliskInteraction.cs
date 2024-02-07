using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObeliskInteraction : Interactable
{
    [SerializeField] private GameObject cube;
    [SerializeField] private ObeliskPuzzle obeliskPuzzle;
    [SerializeField] private float rotationSpeed;
    private bool isRotating = false;
    private float targetAngle;

    public override void Interact()
    {
        if (!isRotating)
        {
            targetAngle = cube.transform.eulerAngles.y + 90f;
            isRotating = true;
        }
    }

    void Update()
    {
        if (isRotating)
        {
            cube.transform.rotation = Quaternion.RotateTowards(cube.transform.rotation, Quaternion.Euler(0, targetAngle, 0), rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(cube.transform.rotation, Quaternion.Euler(0, targetAngle, 0)) < 0.1f)
            {
                cube.transform.rotation = Quaternion.Euler(0, targetAngle, 0);
                obeliskPuzzle.UpdateSelected();
                isRotating = false;
            }
        }
    }
}
