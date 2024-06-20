using System.Collections;
using UnityEngine;

namespace cowsins
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject playerUI;

        [SerializeField] private bool disablePlayerUIWhilePaused;  
        public static PauseMenu Instance { get; private set; }

        public static bool isPaused { get; private set; }

        [HideInInspector]public PlayerStats stats;

        [SerializeField] private CanvasGroup menu;

        [SerializeField] private float fadeSpeed;

        private CanvasGroup mainMenu;
        private CanvasGroup settingsMenu;
        private CanvasGroup videoTab;
        private CanvasGroup audioTab;
        private CanvasGroup controlsTab;
        private GameObject resetButton;
        private GameObject applyButton;


        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;

            isPaused = false;
        }

        private void Start()
        {
            mainMenu = menu.transform.Find("Main").GetComponent<CanvasGroup>();
            settingsMenu = menu.transform.Find("Settings").GetComponent<CanvasGroup>();
            videoTab = settingsMenu.transform.Find("VIDEO TAB").GetComponent<CanvasGroup>();
            audioTab = settingsMenu.transform.Find("AUDIO TAB").GetComponent<CanvasGroup>();
            controlsTab = settingsMenu.transform.Find("CONTROLS TAB").GetComponent<CanvasGroup>();
            resetButton = settingsMenu.transform.Find("TopTabButton | Reset").gameObject;
            applyButton = settingsMenu.transform.Find("TopTabButton | Apply").gameObject;
            menu.gameObject.SetActive(false);
            menu.alpha = 0;
        }

        private void Update()
        {
            if (InputManager.pausing) isPaused = !isPaused;

            if (isPaused)
            {
                stats.LoseControl();
                if (!menu.gameObject.activeSelf)
                {
                    menu.gameObject.SetActive(true);
                    menu.alpha = 0; 
                }
                if (menu.alpha < 1) menu.alpha += Time.deltaTime * fadeSpeed;

                if(disablePlayerUIWhilePaused)playerUI.SetActive(false); 
            }
            else
            {
                menu.alpha -= Time.deltaTime * fadeSpeed;
                if (menu.alpha <= 0)
                {
                    CloseSettings();
                    menu.gameObject.SetActive(false);
                }
                }
        }

        public void UnPause()
        {
            isPaused = false;
            StartCoroutine(CheckIfCanGrantControl());
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            playerUI.SetActive(true);
        }

        public void OpenSettings()
        {
            mainMenu.gameObject.SetActive(false);
            settingsMenu.gameObject.SetActive(true);
            mainMenu.alpha = 0;
            settingsMenu.alpha = 1;
            ResetTabs();
        }
        public void CloseSettings()
        {
            settingsMenu.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
            settingsMenu.alpha = 0;
            mainMenu.alpha = 1;
            ResetTabs();
        }

        public void SwitchVideoTab()
        {
            audioTab.gameObject.SetActive(false);
            audioTab.alpha = 0;
            controlsTab.gameObject.SetActive(false);
            controlsTab.alpha = 0;
            videoTab.gameObject.SetActive(true);
            videoTab.alpha = 1;
            resetButton.SetActive(true);
            applyButton.SetActive(true);
        }
        public void SwitchAudioTab()
        {
            videoTab.gameObject.SetActive(false);
            videoTab.alpha = 0;
            controlsTab.gameObject.SetActive(false);
            controlsTab.alpha = 0;
            audioTab.gameObject.SetActive(true);
            audioTab.alpha = 1;
            resetButton.SetActive(true);
            applyButton.SetActive(true);
        }
        public void SwitchControlsTab()
        {
            videoTab.gameObject.SetActive(false);
            videoTab.alpha = 0;
            audioTab.gameObject.SetActive(false);
            audioTab.alpha = 0;
            controlsTab.gameObject.SetActive(true);
            controlsTab.alpha = 1;
            resetButton.SetActive(true);
            applyButton.SetActive(true);
        }
        public void ResetTabs()
        {
            videoTab.gameObject.SetActive(false);
            videoTab.alpha = 0;
            audioTab.gameObject.SetActive(false);
            audioTab.alpha = 0;
            controlsTab.gameObject.SetActive(false);
            controlsTab.alpha = 0;
            resetButton.SetActive(false);
            applyButton.SetActive(false);
        }

        public void QuitGame() => Application.Quit();

        public void TogglePause()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (disablePlayerUIWhilePaused) playerUI.SetActive(false);
            }
            else
            {
                StartCoroutine(CheckIfCanGrantControl());
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                playerUI.SetActive(true);
            }
        }

        private IEnumerator CheckIfCanGrantControl()
        {
            yield return new WaitForEndOfFrame();
            stats.CheckIfCanGrantControl();
        }

    }
}
