using UnityEngine;
using UnityEngine.SceneManagement;
namespace cowsins {
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup introMenu;

        private bool canLerp = false, waitToLerp;

        private CanvasGroup from, to;
        private GameObject View;

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

    }
}