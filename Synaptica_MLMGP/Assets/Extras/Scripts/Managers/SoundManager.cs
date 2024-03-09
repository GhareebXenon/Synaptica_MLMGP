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
        switch (scene.name)
        {
            case "MainMenu 1":
                    PlayMusic(bgm[0], 0, 0, false, 0, true, "Music");
                    break;
            case "Exposition":
                    PlayMusic(bgm[1], 0, 0, false, 0, true, "Music");
                    break;
            case "Level 1":
                    PlayMusic(bgm[2], 0, 0, false, 0, true, "Music");
                    break;
            case "Level 2":
                    PlayMusic(bgm[2], 0, 0, false, 0, true, "Music");
                    break;
            default:
                    PlayMusic(bgm[0], 0, 0, false, 0, true, "Music");
                    break;
            }
    }

    public void PlaySound(AudioClip clip, float delay = 0, float pitchAdded = 0, bool randomPitch = false, float spatialBlend = 0, bool loopable = false, string output = "Master")
    {
        StartCoroutine(Play(clip,delay,pitchAdded,randomPitch,spatialBlend, loopable,output)); 
    }

    public void PlayMusic(AudioClip clip, float delay = 0, float pitchAdded = 0, bool randomPitch = false, float spatialBlend = 0, bool loopable = true, string output = "Music")
    {
        StartCoroutine(PlayM(clip,delay,pitchAdded,randomPitch,spatialBlend, loopable)); 
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

    private IEnumerator PlayM(AudioClip clip, float delay, float pitch, bool randomPitch,  float spatialBlend, bool loopable)
    {
        if (!clip) yield return null; 
        yield return new WaitForSeconds(delay);
        bgmSrc.loop = loopable;
        bgmSrc.spatialBlend = spatialBlend;
        float pitchAdded = randomPitch ? Random.Range(-pitch, pitch) : pitch; 
        bgmSrc.pitch = 1 + pitchAdded;
        bgmSrc.clip = clip;
        bgmSrc.Play(); 
        yield return null; 
    }
}
}

