using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTrigger : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject generalManagers;
    private bool entered = false;

    private void Start()
    {
        generalManagers = GameObject.Find("GeneralManagers");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !entered)
        {
            StartCoroutine(Countdown());
            doorAnimator.SetBool("IsOpen", false);
            entered = true;
        }
    }

    IEnumerator Countdown()
    {
        Debug.Log("Countdown Started!");
        yield return new WaitForSeconds(30);
        Debug.Log("Countdown Ended!");
        Destroy(generalManagers);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }
}
