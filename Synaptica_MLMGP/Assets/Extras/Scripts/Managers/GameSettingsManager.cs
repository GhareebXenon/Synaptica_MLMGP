using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
namespace cowsins { 
public class GameSettingsManager : MonoBehaviour
{
    
    [SerializeField] private AudioMixer masterMixer; 

    [SerializeField] private GameObject mainMenu;

    [HideInInspector] public int fullScreen;

    [HideInInspector] public int res;

    [HideInInspector] public int maxFrameRate;

    [HideInInspector] public int vsync;

    [HideInInspector] public int graphicsQuality;

    [HideInInspector] public float masterVolume;

    [HideInInspector] public float vocalsVolume;

    [HideInInspector] public float sfxVolume;

    [HideInInspector] public float musicVolume;
        
    public GameObject settingsMenu;

    public Transform videoTab;

    private Transform audioTab;

    private Transform controlsTab;

    public TMP_Dropdown frameRateDropdown, resolutionRateDropdown, graphicsDropdown;

    private UnityEngine.UI.Toggle fullScreenToggle,vsyncToggle;

    private UnityEngine.UI.Slider masterVolumeSlider;

    private UnityEngine.UI.Slider vocalsVolumeSlider;

    private UnityEngine.UI.Slider sfxVolumeSlider;

    private UnityEngine.UI.Slider musicVolumeSlider;

    public GameObject rebindOverlay;

    private Button movementForwardButton;
    private Button movementBackwardButton;
    private Button movementLeftButton;
    private Button movementRightButton;
    private Button sprintingButton;
    private Button crouchingButton;
    private Button jumpingButton;
    private Button interactingButton;
    private Button firingButton;
    private Button aimingButton; 
    private Button meleeButton; 
    private Button dropButton; 

    private TextMeshProUGUI rebindPrompt;

    private TextMeshProUGUI movementForwardText;
    private TextMeshProUGUI movementBackwardText;
    private TextMeshProUGUI movementLeftText;
    private TextMeshProUGUI movementRightText;
    private TextMeshProUGUI sprintingText;
    private TextMeshProUGUI crouchingText;
    private TextMeshProUGUI jumpingText;
    private TextMeshProUGUI interactingText;
    private TextMeshProUGUI firingText;
    private TextMeshProUGUI aimingText;
    private TextMeshProUGUI meleeText;
    private TextMeshProUGUI dropText;

    private int width = 1920, height = 1080;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
            if (mainMenu != null) mainMenu.SetActive(true);
            settingsMenu = GameObject.FindGameObjectWithTag("SettingsMenu");
            videoTab = settingsMenu.transform.Find("VIDEO TAB");
            audioTab = settingsMenu.transform.Find("AUDIO TAB");
            controlsTab = settingsMenu.transform.Find("CONTROLS TAB");
            rebindOverlay = settingsMenu.transform.Find("RebindOverlay").GameObject();
            frameRateDropdown = videoTab.Find("SettingsButton @ Target Framerate").Find("Container").Find("Dropdown").GetComponent<TMP_Dropdown>();
            resolutionRateDropdown = videoTab.Find("SettingsButton @ Resolution").Find("Container").Find("Dropdown").GetComponent<TMP_Dropdown>();
            graphicsDropdown = videoTab.Find("SettingsButton @ Graphics Quality").Find("Container").Find("Dropdown").GetComponent<TMP_Dropdown>();
            fullScreenToggle = videoTab.Find("SettingsButton @ FullScreen").Find("Container").Find("Toggle").GetComponent<Toggle>();
            vsyncToggle = videoTab.Find("SettingsButton @ VSync").Find("Container").Find("Toggle").GetComponent<Toggle>();
            masterVolumeSlider = audioTab.Find("SettingsButton @ MasterVolume").Find("Container").Find("Slider").GetComponent<Slider>();
            vocalsVolumeSlider = audioTab.Find("SettingsButton @ VocalsVolume").Find("Container").Find("Slider").GetComponent<Slider>();
            sfxVolumeSlider = audioTab.Find("SettingsButton @ SfxVolume").Find("Container").Find("Slider").GetComponent<Slider>();
            musicVolumeSlider = audioTab.Find("SettingsButton @ MusicVolume").Find("Container").Find("Slider").GetComponent<Slider>();
            movementForwardButton = controlsTab.Find("SettingsButton @ MovementForward").Find("RebindButton").GetComponent<Button>();
            movementBackwardButton = controlsTab.Find("SettingsButton @ MovementBackward").Find("RebindButton").GetComponent<Button>();
            movementLeftButton = controlsTab.Find("SettingsButton @ MovementLeft").Find("RebindButton").GetComponent<Button>();
            movementRightButton = controlsTab.Find("SettingsButton @ MovementRight").Find("RebindButton").GetComponent<Button>();
            sprintingButton = controlsTab.Find("SettingsButton @ Sprinting").Find("RebindButton").GetComponent<Button>();
            crouchingButton = controlsTab.Find("SettingsButton @ Crouching").Find("RebindButton").GetComponent<Button>();
            jumpingButton = controlsTab.Find("SettingsButton @ Jumping").Find("RebindButton").GetComponent<Button>();
            interactingButton = controlsTab.Find("SettingsButton @ Interacting").Find("RebindButton").GetComponent<Button>();
            firingButton = controlsTab.Find("SettingsButton @ Firing").Find("RebindButton").GetComponent<Button>();
            aimingButton = controlsTab.Find("SettingsButton @ Aiming").Find("RebindButton").GetComponent<Button>();
            meleeButton = controlsTab.Find("SettingsButton @ Melee").Find("RebindButton").GetComponent<Button>();
            dropButton = controlsTab.Find("SettingsButton @ Drop").Find("RebindButton").GetComponent<Button>();

            rebindPrompt = rebindOverlay.transform.Find("RebindPrompt").GetComponent<TextMeshProUGUI>();
            movementForwardText = movementForwardButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            movementBackwardText = movementBackwardButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            movementLeftText = movementLeftButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            movementRightText = movementRightButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            sprintingText = sprintingButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            crouchingText = crouchingButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            jumpingText = jumpingButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            interactingText = interactingButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            firingText = firingButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            aimingText = aimingButton.transform.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            meleeText = meleeButton.transform.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();
            dropText = dropButton.transform.Find("RebindText").GetComponent<TextMeshProUGUI>();

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

            movementForwardButton.onClick.AddListener(delegate
            {
                movementForwardButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Move Forward)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Movement.PerformInteractiveRebinding(1)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    movementForwardButton.interactable = true;
                }).Start();
            });
            movementBackwardButton.onClick.AddListener(delegate
            {
                movementBackwardButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Move Backward)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Movement.PerformInteractiveRebinding(2)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    movementBackwardButton.interactable = true;
                }).Start();
            });
            movementLeftButton.onClick.AddListener(delegate
            {
                movementLeftButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Move Left)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Movement.PerformInteractiveRebinding(3)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    movementLeftButton.interactable = true;
                }).Start();
            });
            movementRightButton.onClick.AddListener(delegate
            {
                movementRightButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Move Right)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Movement.PerformInteractiveRebinding(4)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    movementRightButton.interactable = true;
                }).Start();
            });
            sprintingButton.onClick.AddListener(delegate
            {
                sprintingButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Sprint)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Sprinting.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    sprintingButton.interactable = true;
                }).Start();
            });
            crouchingButton.onClick.AddListener(delegate
            {
                crouchingButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Crouch)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Crouching.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    crouchingButton.interactable = true;
                }).Start();
            });
            jumpingButton.onClick.AddListener(delegate
            {
                jumpingButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Jump)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Jumping.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    jumpingButton.interactable = true;
                }).Start();
            });
            interactingButton.onClick.AddListener(delegate
            {
                interactingButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Interact)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Interacting.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    interactingButton.interactable = true;
                }).Start();
            });
            firingButton.onClick.AddListener(delegate
            {
                firingButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Fire)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Firing.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    firingButton.interactable = true;
                }).Start();
            });
            aimingButton.onClick.AddListener(delegate
            {
                aimingButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Aim)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Aiming.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    aimingButton.interactable = true;
                }).Start();
            });
            meleeButton.onClick.AddListener(delegate
            {
                meleeButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Melee)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Melee.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    meleeButton.interactable = true;
                }).Start();
            });
            dropButton.onClick.AddListener(delegate
            {
                dropButton.interactable = false;
                InputManager.inputActions.GameControls.Disable();
                rebindPrompt.text = rebindPrompt.text.Replace("(binding)", "(Drop)");
                rebindOverlay.SetActive(true);
                InputManager.inputActions.GameControls.Drop.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    rebindOverlay.SetActive(false);
                    InputManager.inputActions.GameControls.Enable();
                    meleeButton.interactable = true;
                }).Start();
            });

            if (mainMenu != null) mainMenu.SetActive(false);
            settingsMenu.SetActive(false);
            rebindOverlay.SetActive(false);
        }

    private void Start()
    {
            LoadSettings(); // Load our settings
    }

    private void Update()
    {
            if (settingsMenu.activeInHierarchy)
            {
                // Change the volume
                masterMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20);
                masterMixer.SetFloat("vocalsVolume", Mathf.Log10(vocalsVolume) * 20);
                masterMixer.SetFloat("sfxVolume", Mathf.Log10(sfxVolume) * 20);
                masterMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);

                // Change the bindings
                movementForwardText.text = InputManager.inputActions.GameControls.Movement.bindings[1].ToDisplayString();
                movementBackwardText.text = InputManager.inputActions.GameControls.Movement.bindings[2].ToDisplayString();
                movementLeftText.text = InputManager.inputActions.GameControls.Movement.bindings[3].ToDisplayString();
                movementRightText.text = InputManager.inputActions.GameControls.Movement.bindings[4].ToDisplayString();
                sprintingText.text = InputManager.inputActions.GameControls.Sprinting.bindings[0].ToDisplayString();
                crouchingText.text = InputManager.inputActions.GameControls.Crouching.bindings[0].ToDisplayString();
                jumpingText.text = InputManager.inputActions.GameControls.Jumping.bindings[0].ToDisplayString();
                interactingText.text = InputManager.inputActions.GameControls.Interacting.bindings[0].ToDisplayString();
                firingText.text = InputManager.inputActions.GameControls.Firing.bindings[0].ToDisplayString();
                aimingText.text = InputManager.inputActions.GameControls.Aiming.bindings[0].ToDisplayString();
                meleeText.text = InputManager.inputActions.GameControls.Melee.bindings[0].ToDisplayString();
                dropText.text = InputManager.inputActions.GameControls.Drop.bindings[0].ToDisplayString();
            }
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
        PlayerPrefs.SetString("playerInput", InputManager.inputActions.SaveBindingOverridesAsJson());
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        // Video
        if (PlayerPrefs.HasKey("res"))
            res = PlayerPrefs.GetInt("res");
        if (PlayerPrefs.HasKey("fullScreen"))
            fullScreen = PlayerPrefs.GetInt("fullScreen");
        if (PlayerPrefs.HasKey("maxFrameRate"))
            maxFrameRate = PlayerPrefs.GetInt("maxFrameRate");
        if (PlayerPrefs.HasKey("vsync"))
            vsync = PlayerPrefs.GetInt("vsync");
        if (PlayerPrefs.HasKey("graphicsQuality"))
            graphicsQuality = PlayerPrefs.GetInt("graphicsQuality");
        // Audio
        if (PlayerPrefs.HasKey("masterVolume"))
            masterVolume = PlayerPrefs.GetFloat("masterVolume");
        if (PlayerPrefs.HasKey("vocalsVolume"))
            vocalsVolume = PlayerPrefs.GetFloat("vocalsVolume");
        if (PlayerPrefs.HasKey("sfxVolume"))
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        if (PlayerPrefs.HasKey("musicVolume"))
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        // Controls
        if (PlayerPrefs.HasKey("playerInput")) 
            InputManager.inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("playerInput"));
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