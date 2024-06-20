using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTrigger : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject generalManagers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(countdown());
            doorAnimator.SetBool("IsOpen", false);
        }

        IEnumerator countdown()
        {
            yield return new WaitForSeconds(90);
            Destroy(generalManagers);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            yield return null;
        }
    }
}
