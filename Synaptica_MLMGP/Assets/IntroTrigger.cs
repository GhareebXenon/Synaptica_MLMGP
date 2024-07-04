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
        playerMovement.LookAt(boss, 1.6f);
        introTimeline.Play();
        while (introTimeline.state == PlayState.Playing)
        {
            yield return null;
        }
        playerMovement.StopLookingAt();
        weaponController.canShoot = true;
        yield return null;
    }
}
