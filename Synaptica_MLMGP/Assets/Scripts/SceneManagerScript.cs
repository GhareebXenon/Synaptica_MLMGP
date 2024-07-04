using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

namespace cowsins
{
    public class SceneManagerScript : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] string SceneName;
        [SerializeField] private Slider loadingSlider;
        [SerializeField] private GameObject LoadingScreen;
  
      
        private void Start()
        {
            StartCoroutine(SkipAfterVideo());
        }
        private void Update()
        {
          
            if (videoPlayer != null && videoPlayer.isPlaying && InputManager.inputActions.GameControls.Pause.ReadValue<float>() > 0.1)
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

            LoadingScreen.SetActive(true);
            StartCoroutine(LoadAsynchronos());
        }
        public void EndSceneSwitch()
        {
            StartCoroutine(EndSceneSwitchCoroutine());
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
        
        private IEnumerator LoadAsynchronos()
        {
            AsyncOperation loadOpertaion = SceneManager.LoadSceneAsync(SceneName);
            while(!loadOpertaion.isDone)
            {
                float progressValue = Mathf.Clamp01(loadOpertaion.progress / 0.9f);
                loadingSlider.value = progressValue;
                yield return null;
            }
            
        }

        private IEnumerator EndSceneSwitchCoroutine()
        {
            SoundManager.Instance.FadeOutMusic(6);
            yield return new WaitForSeconds(6.5f);
            Destroy(SoundManager.Instance.gameObject);
            SceneManager.LoadScene(SceneName);
            StartCoroutine(LoadAsynchronos());
            yield return null;
        }
    }
}
