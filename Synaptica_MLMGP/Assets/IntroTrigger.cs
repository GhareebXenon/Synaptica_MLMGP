using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform boss;
    [SerializeField] private PlayableDirector introTimeline;

    private PlayerMovement playerMovement;
    private WeaponController weaponController;

    private bool played = false;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        weaponController = player.GetComponent<WeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartIntro();
    }

    public void StartIntro()
    {
        if (played) return;
        StartCoroutine(Begin());
        played = true;
    }

    private IEnumerator Begin()
    {
        weaponController.canShoot = false;
        playerMovement.xRotationMin = -15f;
        playerMovement.xRotationMax = 5f;
        playerMovement.LookAt(boss, 1.6f);
        introTimeline.Play();
        while (introTimeline.state == PlayState.Playing)
        {
            yield return null;
        }
        playerMovement.StopLookingAt();
        playerMovement.xRotationMin = -89.7f;
        playerMovement.xRotationMax = 89.7f;
        weaponController.canShoot = true;
        yield return null;
    }
}
