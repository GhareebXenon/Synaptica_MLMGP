using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] string SceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneSwitch();


    }
    public void SceneSwitch()
    {
        SceneManager.LoadScene(SceneName);
    }
}
