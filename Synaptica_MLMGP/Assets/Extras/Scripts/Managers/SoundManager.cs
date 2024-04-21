using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Unity.VisualScripting;
using static UnityEditor.Rendering.CameraUI;
using static Unity.VisualScripting.Member;
namespace cowsins
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        [SerializeField] private AudioClip[] bgm;
        [SerializeField] private AudioMixer audioMixer;

        private AudioSource src;
        private AudioSource bgmSrc;
        private AudioSource bgmSrcSec;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(this.gameObject);

            CheckAudioSources();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            bgmSrc?.Stop();
            switch (scene.name)
            {
                case "MainMenu 1":
                    PlayMusicFadeIn(bgm[0], 0.4f, 1);
                    break;
                case "Exposition":
                    PlayMusicFadeIn(bgm[1], 1, 1);
                    break;
                case "Level 1":
                    PlayMusicFadeIn(bgm[2], 0.25f, 1);
                    break;
                case "Level 2":
                    PlayMusicFadeIn(bgm[3], 0.3f, 0.33f);
                    break;
                case "Level 3":
                    PlayMusicFadeIn(bgm[2], 0.25f, 1);
                    break;
                case "Level 4":
                    PlayMusicFadeIn(bgm[4], 0.25f, 1);
                    break;
                default:
                    PlayMusicFadeIn(bgm[2], 0.25f, 1);
                    break;
            }
        }

        public void PlaySound(AudioClip clip, float delay = 0, float pitchAdded = 0, bool randomPitch = false, float spatialBlend = 0, bool loopable = false, string output = "Master")
        {
            StartCoroutine(Play(clip, delay, pitchAdded, randomPitch, spatialBlend, loopable, output));
        }

        public void PlayMusic(AudioClip clip, float volume = 1, float delay = 0, bool loopable = true)
        {
            StartCoroutine(PlayM(clip, volume, delay, loopable));
        }

        public void PlayMusicFadeIn(AudioClip clip, float volume = 1, float fadeDuration = 1.5f, bool loopable = true)
        {
            StartCoroutine(PlayMFadeIn(clip, volume, fadeDuration, loopable));
        }

        public AudioSource GetMusicSource()
        {
            if (bgmSrc != null && bgmSrc.isPlaying)
            {
                return bgmSrc;
            }
            else if (bgmSrcSec != null && bgmSrcSec.isPlaying)
            {
                return bgmSrcSec;
            }
            else
            {
                return null;
            }
        }

        private void CheckAudioSources()
        {
            if (GetComponent<AudioSource>() == null)
            {
                src = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                src = GetComponent<AudioSource>();
            }
            if (transform.Find("BGM").GetComponent<AudioSource>() == null)
            {
                bgmSrc = transform.Find("BGM").AddComponent<AudioSource>();
                bgmSrcSec = transform.Find("BGM").AddComponent<AudioSource>();
            }
            else if (transform.Find("BGM").GetComponents<AudioSource>().Length == 1)
            {
                bgmSrc = transform.Find("BGM").GetComponent<AudioSource>();
                bgmSrcSec = transform.Find("BGM").AddComponent<AudioSource>();
            }
            else if (transform.Find("BGM").GetComponents<AudioSource>().Length == 2)
            {
                bgmSrc = transform.Find("BGM").GetComponents<AudioSource>()[0];
                bgmSrcSec = transform.Find("BGM").GetComponents<AudioSource>()[1];
            }

            bgmSrc.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
            bgmSrcSec.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        }

        private IEnumerator Play(AudioClip clip, float delay, float pitch, bool randomPitch, float spatialBlend, bool loopable, string output)
        {
            if (!clip) yield return null;
            yield return new WaitForSeconds(delay);
            src.outputAudioMixerGroup = audioMixer.FindMatchingGroups(output)[0];
            src.loop = loopable;
            src.spatialBlend = spatialBlend;
            float pitchAdded = randomPitch ? Random.Range(-pitch, pitch) : pitch;
            src.pitch = 1 + pitchAdded;
            src.PlayOneShot(clip);
            yield return null;
        }

        private IEnumerator PlayM(AudioClip clip, float volume, float delay, bool loopable)
        {
            if (!clip) yield return null;
            yield return new WaitForSeconds(delay);
            bgmSrc.volume = volume;
            bgmSrc.loop = loopable;
            bgmSrc.clip = clip;
            bgmSrc.Play();
            yield return null;
        }

        private IEnumerator PlayMFadeIn(AudioClip clip, float volume, float fadeDuration, bool loopable)
        {
            if (clip == null || fadeDuration < 0 || (bgmSrc == null && bgmSrcSec == null))
            {
                Debug.LogError("Invalid parameters for PlayMFadeIn coroutine.");
                yield break;
            }

            AudioSource fadeInSource = bgmSrc != null && bgmSrc.isPlaying ? bgmSrc : bgmSrcSec;
            AudioSource fadeOutSource = bgmSrcSec != null && bgmSrcSec.isPlaying ? bgmSrcSec : bgmSrc;

            float targetVolume = volume;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                fadeOutSource.volume = Mathf.Lerp(volume, 0, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            fadeOutSource.Stop();
            fadeOutSource.volume = 0;

            fadeInSource.clip = clip;
            fadeInSource.loop = loopable;
            fadeInSource.Play();
            elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                fadeInSource.volume = Mathf.Lerp(0, targetVolume, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            fadeInSource.volume = targetVolume;
        }

    }
}

