using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
namespace cowsins { 
public class GameSettingsManager : MonoBehaviour
{
    [HideInInspector] public int fullScreen;

    [HideInInspector] public int res;

    [HideInInspector] public int maxFrameRate;

    [HideInInspector] public int vsync;

    [HideInInspector] public int graphicsQuality;

    [HideInInspector] public float masterVolume;

    [HideInInspector] public float vocalsVolume;

    [HideInInspector] public float sfxVolume;

    [HideInInspector] public float musicVolume;

    [SerializeField] private TMP_Dropdown frameRateDropdown, resolutionRateDropdown, graphicsDropdown;

    [SerializeField] private UnityEngine.UI.Toggle fullScreenToggle,vsyncToggle;

    [SerializeField] private UnityEngine.UI.Slider masterVolumeSlider;

    [SerializeField] private UnityEngine.UI.Slider vocalsVolumeSlider;

    [SerializeField] private UnityEngine.UI.Slider sfxVolumeSlider;

    [SerializeField] private UnityEngine.UI.Slider musicVolumeSlider;

    [SerializeField] private AudioMixer masterMixer; 


    private int width = 1920, height = 1080;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        LoadSettings(); // Load our settings 

        // Handle UI Elements for the Settings Menu
        frameRateDropdown.onValueChanged.AddListener(delegate
        {
            maxFrameRate = OnDropDownChanged(frameRateDropdown);
        });
        resolutionRateDropdown.onValueChanged.AddListener(delegate
        {
            res = OnDropDownChanged(resolutionRateDropdown);
        });
        graphicsDropdown.onValueChanged.AddListener(delegate
        {
            graphicsQuality = OnDropDownChanged(graphicsDropdown);
        });
        fullScreenToggle.onValueChanged.AddListener(delegate
        {
            fullScreen = OnDropDownChanged(fullScreenToggle);
        });
        fullScreenToggle.onValueChanged.AddListener(delegate
        {
            vsync = OnDropDownChanged(vsyncToggle);
        });
        masterVolumeSlider.onValueChanged.AddListener(delegate
        {
            masterVolume = OnDropDownChanged(masterVolumeSlider);
            masterMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20);
        });
        vocalsVolumeSlider.onValueChanged.AddListener(delegate
        {
            vocalsVolume = OnDropDownChanged(vocalsVolumeSlider);
            masterMixer.SetFloat("vocalsVolume", Mathf.Log10(vocalsVolume) * 20);
        });
        sfxVolumeSlider.onValueChanged.AddListener(delegate
        {
            sfxVolume = OnDropDownChanged(sfxVolumeSlider);
            masterMixer.SetFloat("sfxVolume", Mathf.Log10(sfxVolume) * 20);
        });
        musicVolumeSlider.onValueChanged.AddListener(delegate
        {
            musicVolume = OnDropDownChanged(musicVolumeSlider);
            masterMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
        });
    }

    private void Update()
    {
        // Change the volume
        masterMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20);
        masterMixer.SetFloat("vocalsVolume", Mathf.Log10(vocalsVolume) * 20);
        masterMixer.SetFloat("sfxVolume", Mathf.Log10(sfxVolume) * 20);
        masterMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
    }

    public void SetWindowedScreen() => fullScreen = 0;

    public void SetFullScreen() => fullScreen = 1;

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("res", res);
        PlayerPrefs.SetInt("fullScreen", fullScreen);
        PlayerPrefs.SetInt("maxFrameRate", maxFrameRate);
        PlayerPrefs.SetInt("vsync", vsync);
        PlayerPrefs.SetInt("graphicsQuality", graphicsQuality);
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("vocalsVolume", vocalsVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }

    public void LoadSettings()
    {
        // Video
        res = PlayerPrefs.GetInt("res");
        fullScreen = PlayerPrefs.GetInt("fullScreen");
        maxFrameRate = PlayerPrefs.GetInt("maxFrameRate");
        vsync = PlayerPrefs.GetInt("vsync");
        graphicsQuality = PlayerPrefs.GetInt("graphicsQuality");
        // Audio
        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        vocalsVolume = PlayerPrefs.GetFloat("vocalsVolume");
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        // Video
        switch (maxFrameRate)
        {
            case 0: Application.targetFrameRate = 60; break;
            case 1: Application.targetFrameRate = 120; break;
            case 2: Application.targetFrameRate = 230; break;
            case 3: Application.targetFrameRate = 300; break;
        }
        
        switch (res)
        {
            case 0:
                width = 1920;
                height = 1080; 
                break;
            case 1:
                width = 1920;
                height = 720;

                break;
            case 2:
                width = 854;
                height = 480;
                break;
        }
        Screen.SetResolution(width, height, fullScreen == 1 ? true : false);
        QualitySettings.vSyncCount = vsync;
        QualitySettings.SetQualityLevel(graphicsQuality);
        //Audio
        masterMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20);
        masterMixer.SetFloat("vocalsVolume", Mathf.Log10(vocalsVolume) * 20);
        masterMixer.SetFloat("sfxVolume", Mathf.Log10(sfxVolume) * 20);
        masterMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
        // Video UI
        frameRateDropdown.value = maxFrameRate;
        resolutionRateDropdown.value = res;
        graphicsDropdown.value = graphicsQuality;
        fullScreenToggle.isOn = fullScreen == 1 ? true : false; 
        vsyncToggle.isOn = vsync == 1 ? true : false;
        masterVolumeSlider.value = masterVolume; 
    }

    public void ResetSettings()
    {
        res = 0; 
        fullScreen = 1;
        maxFrameRate = frameRateDropdown.options.Count - 1;  
        vsync = 0;
        graphicsQuality = 2;
        masterVolume = 1; 
        SaveSettings();
        LoadSettings(); 
    }
    public int OnDropDownChanged(TMP_Dropdown dropDown)
    {
        return dropDown.value;
    }
    public int OnDropDownChanged(UnityEngine.UI.Toggle toggle)
    {
        return toggle.isOn ? 1 : 0;
    }
    public float OnDropDownChanged(UnityEngine.UI.Slider slider)
    {
        return slider.value;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        PlayerPrefs.Save(); 
    }
}
}