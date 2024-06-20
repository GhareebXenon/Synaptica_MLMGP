#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
namespace cowsins {
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup introMenu;

        private bool canLerp = false, waitToLerp;

        private CanvasGroup from, to;
        private GameObject View;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private CanvasGroup settingsMenu;
        private CanvasGroup videoTab;
        private CanvasGroup audioTab;
        private CanvasGroup controlsTab;
        private GameObject resetButton;
        private GameObject applyButton;

        private void Start()
        {
            mainMenu.SetActive(false);
            videoTab = settingsMenu.transform.Find("VIDEO TAB").GetComponent<CanvasGroup>();
            audioTab = settingsMenu.transform.Find("AUDIO TAB").GetComponent<CanvasGroup>();
            controlsTab = settingsMenu.transform.Find("CONTROLS TAB").GetComponent<CanvasGroup>();
            resetButton = settingsMenu.transform.Find("TopTabButton | Reset").gameObject;
            applyButton = settingsMenu.transform.Find("TopTabButton | Apply").gameObject;
            ResetTabs();
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }

        private void Update()
        {
            if (!canLerp) return;

            to.gameObject.SetActive(true);
            from.alpha -= Time.deltaTime;
            if (from.alpha <= .2f && waitToLerp || !waitToLerp) to.alpha += Time.deltaTime;

            if (introMenu.alpha == 0)
                from.gameObject.SetActive(false);
        }
        public void ChangeMenu() => canLerp = true;

        public void SetFrom(CanvasGroup From) => from = From;

        public void SetTo(CanvasGroup To) => to = To;

        public void WaitToLerp(bool boolean) => waitToLerp = boolean;

        public void ChangeScene(int scene) => SceneManager.LoadScene(scene);

        public void DeActivate(GameObject view) 
        {
            
            view.SetActive(false);
        }
        public void Activate(GameObject view) =>view.SetActive(true);
        public void QuitGame() {
            Debug.Log("Exited");
            Application.Quit();
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

        public void ActivateSettingsButtons()
        {
            resetButton.SetActive(true);
            applyButton.SetActive(true);
        }
    }
}
#endif