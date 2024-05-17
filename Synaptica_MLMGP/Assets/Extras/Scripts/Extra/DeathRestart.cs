using UnityEngine;
using UnityEngine.SceneManagement;
namespace cowsins { 
public class DeathRestart : MonoBehaviour
{
    private void Update()
    {
        if (InputManager.reloading) RestartGame(); 
    }

    public void RestartGame()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
}