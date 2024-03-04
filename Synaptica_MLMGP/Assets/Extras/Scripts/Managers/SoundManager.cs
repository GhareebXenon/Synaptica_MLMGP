using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
namespace cowsins {
public class SoundManager :MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioClip[] bgm;
    [SerializeField] private AudioMixer audioMixer;

    private AudioSource src;
    private AudioSource bgmSrc;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null; 
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject); 

        src = GetComponent<AudioSource>();
        bgmSrc = transform.Find("BGM").GetComponent<AudioSource>();
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
        bgmSrc.Stop();
        if (bgm[scene.buildIndex] != null)
        {
            PlayMusic(bgm[scene.buildIndex], 0, 0, false, 0, true, "Music");
        }
    }

    public void PlaySound(AudioClip clip, float delay = 0, float pitchAdded = 0, bool randomPitch = false, float spatialBlend = 0, bool loopable = false, string output = "Master")
    {
        StartCoroutine(Play(clip,delay,pitchAdded,randomPitch,spatialBlend, loopable,output)); 
    }

    public void PlayMusic(AudioClip clip, float delay = 0, float pitchAdded = 0, bool randomPitch = false, float spatialBlend = 0, bool loopable = false, string output = "Master")
    {
        StartCoroutine(PlayM(clip,delay,pitchAdded,randomPitch,spatialBlend, loopable,output)); 
    }

    private IEnumerator Play(AudioClip clip, float delay, float pitch, bool randomPitch,  float spatialBlend, bool loopable, string output)
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

    private IEnumerator PlayM(AudioClip clip, float delay, float pitch, bool randomPitch,  float spatialBlend, bool loopable, string output)
    {
        if (!clip) yield return null; 
        yield return new WaitForSeconds(delay);
        bgmSrc.outputAudioMixerGroup = audioMixer.FindMatchingGroups(output)[0];
        bgmSrc.loop = loopable;
        bgmSrc.spatialBlend = spatialBlend;
        float pitchAdded = randomPitch ? Random.Range(-pitch, pitch) : pitch; 
        bgmSrc.pitch = 1 + pitchAdded;
        bgmSrc.PlayOneShot(clip); 
        yield return null; 
    }
}
}

