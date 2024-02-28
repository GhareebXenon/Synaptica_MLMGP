using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveInteractable : Interactable
{
    [SerializeField] private AudioClip explosivePlantingSFX, explosionSFX;
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private string mission;

    private void Start()
    {
        transform.Find("Explosive").gameObject.SetActive(false);
    }

    public override void Interact()
    {
        transform.Find("Explosive").gameObject.SetActive(true);
        SoundManager.Instance.PlaySound(explosivePlantingSFX, 0, 0, false, 0.25f);
        missionManager.UpdateMission(mission, 1);
        GetComponent<BoxCollider>().enabled = false;
        gameObject.layer = 11;
    }
}
