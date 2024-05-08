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
    private AudioSource audioSource;

    public override void Interact()
    {
        if (!isRotating)
        {
            targetAngle = cube.transform.eulerAngles.y + 90f;
            isRotating = true;
            PlaySfx();
        }
    }

    private void Start()
    {
        audioSource = cube.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isRotating)
        {
            cube.transform.rotation = Quaternion.RotateTowards(cube.transform.rotation, Quaternion.Euler(0, targetAngle, 0), rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(cube.transform.rotation, Quaternion.Euler(0, targetAngle, 0)) < 0.15f)
            {
                cube.transform.rotation = Quaternion.Euler(0, targetAngle, 0);
                audioSource.Stop();
                obeliskPuzzle.UpdateSelected();
                isRotating = false;
            }
        }
    }

    private void PlaySfx()
    {
        audioSource.clip = obeliskPuzzle.rotationSfx[Random.Range(0, obeliskPuzzle.rotationSfx.Count)];
        audioSource.pitch = Random.Range(0.85f, 1.05f);
        audioSource.Play();
    }
}
