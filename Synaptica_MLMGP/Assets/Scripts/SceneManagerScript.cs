using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace cowsins
{
    public class SceneManagerScript : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] string SceneName;
        private void Start()
        {
            StartCoroutine(SkipAfterVideo());
        }
        private void Update()
        {
            if (videoPlayer != null && videoPlayer.isPlaying && InputManager.inputActions.GameControls.Pause.ReadValue<float>() > 0)
            {
                SceneSwitch();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                SceneSwitch();


        }
        public void SceneSwitch()
        {
            SceneManager.LoadScene(SceneName);
        }
        private IEnumerator SkipAfterVideo()
        {
            yield return new WaitForSeconds(1);
            if (videoPlayer != null)
            {
                while (videoPlayer.isPlaying)
                {
                    yield return null;
                }
                SceneSwitch();
            }
        }
    }
}
