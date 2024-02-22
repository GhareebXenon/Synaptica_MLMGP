using cowsins;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NavKeypad { 
public class Keypad : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<string, float> onAccessGranted;
    [SerializeField] private UnityEvent onAccessDenied;
    [SerializeField] private string mission;
    [Header("Combination Code (9 Numbers Max)")]
    public int keypadCombo = 12345;

    public UnityEvent<string, float> OnAccessGranted => onAccessGranted;
    public UnityEvent OnAccessDenied => onAccessDenied;

    [Header("Settings")]
    [SerializeField] private string accessGrantedText = "Granted";
    [SerializeField] private string accessDeniedText = "Denied";

    [Header("Visuals")]
    [SerializeField] private float displayResultTime = 1f;
    [Range(0,5)]
    [SerializeField] private float screenIntensity = 2.5f;
    [Header("Colors")]
    [SerializeField] private Color screenNormalColor = new (0.98f, 0.50f, 0.032f, 1f); //orangy
    [SerializeField] private Color screenDeniedColor = new(1f, 0f, 0f, 1f); //red
    [SerializeField] private Color screenGrantedColor = new(0f, 0.62f, 0.07f); //greenish
    [Header("SoundFx")]
    [SerializeField] private AudioClip buttonClickedSfx;
    [SerializeField] private AudioClip accessDeniedSfx;
    [SerializeField] private AudioClip accessGrantedSfx;
    [Header("Component References")]
    [SerializeField] private Renderer panelMesh;
    [SerializeField] private TMP_Text keypadDisplayText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerUI;
    
    private GameObject keypadCamera;
    private string currentInput;
    private bool displayingResult = false;
    private bool accessWasGranted = false;

    private void Awake()
    {
        keypadCamera = transform.Find("KeypadCamera").gameObject;
        ClearInput();
        panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);
    }


    //Gets value from pressedbutton
    public void AddInput(string input)
    {
        audioSource.PlayOneShot(buttonClickedSfx);
        if (displayingResult || accessWasGranted) return;
        switch (input)
        {
            case "enter":
                CheckCombo();
                break;
            default:
                if (currentInput != null && currentInput.Length == 9) // 9 max passcode size 
                {
                    return;
                }
                currentInput += input;
                keypadDisplayText.text = currentInput;
                break;
        }
        
    }
    public void CheckCombo()
    {
        if(int.TryParse(currentInput, out var currentKombo))
        {
            bool granted = currentKombo == keypadCombo;
            if (!displayingResult)
            {
                StartCoroutine(DisplayResultRoutine(granted));
            }
        }
        else
        {
            Debug.LogWarning("Couldn't process input for some reason..");
        }

    }

    //mainly for animations 
    private IEnumerator DisplayResultRoutine(bool granted)
    {
        displayingResult = true;

        if (granted) AccessGranted();
        else AccessDenied();

        yield return new WaitForSeconds(displayResultTime);
        displayingResult = false;
        if (granted) yield break;
        ClearInput();
        panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);

    }

    private void AccessDenied()
    {
        keypadDisplayText.text = accessDeniedText;
        onAccessDenied?.Invoke();
        panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
        audioSource.PlayOneShot(accessDeniedSfx);
    }

    private void ClearInput()
    {
        currentInput = "";
        keypadDisplayText.text = currentInput;
    }

    private void AccessGranted()
    {
        accessWasGranted = true;
        keypadDisplayText.text = accessGrantedText;
        onAccessGranted?.Invoke(mission, 1);
        panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
        audioSource.PlayOneShot(accessGrantedSfx);
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        mainCamera.SetActive(true);
        keypadCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerUI.SetActive(true);
    }


    }
}