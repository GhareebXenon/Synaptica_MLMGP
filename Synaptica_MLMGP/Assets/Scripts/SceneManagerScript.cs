using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace cowsins
{
    public class SceneManagerScript : MonoBehaviour
    {
        private VideoPlayer player;
        [SerializeField] string SceneName;
        private void Start()
        {
            player = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
            StartCoroutine(SkipAfterVideo());
        }
        private void Update()
        {
            if (player != null && player.isPlaying && InputManager.inputActions.GameControls.Pause.ReadValue<float>() > 0)
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
            if (player != null)
            {
                while (player.isPlaying)
                {
                    yield return null;
                }
                SceneSwitch();
            }
        }
    }
}
