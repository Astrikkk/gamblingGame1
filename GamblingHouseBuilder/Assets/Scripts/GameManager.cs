using UnityEngine;
using UnityEngine.SceneManagement;

//Scene Control
public class GameManager : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player=GameObject.FindObjectOfType<Player>();
    }
    public void LoadScene(string sceneName)
    {
        player.Save();
        SceneManager.LoadScene(sceneName); // Завантажуємо задану сцену
    }

    public void QuitGame()
    {
        player.Save();
        Application.Quit(); // Закриваємо додаток (працює тільки в збірці)
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Отримуємо поточну сцену
        SceneManager.LoadScene(currentScene.name); // Завантажуємо знову поточну сцену
        player.Save();
    }
}
