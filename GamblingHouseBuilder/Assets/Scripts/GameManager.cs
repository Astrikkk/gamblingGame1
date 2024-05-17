using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Scene Control
public class GameManager : MonoBehaviour
{
    public GameObject NotEnoughMoneyObj;
    public TMP_InputField DiamondsInputField;

    public void LoadScene(string sceneName)
    {
        Player.Save();
        SceneManager.LoadScene(sceneName); // Завантажуємо задану сцену
    }

    public void QuitGame()
    {
        Player.Save();
        Application.Quit(); // Закриваємо додаток (працює тільки в збірці)
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Отримуємо поточну сцену
        SceneManager.LoadScene(currentScene.name); // Завантажуємо знову поточну сцену
        Player.Save();
    }

    public void NotEnoughMoneyText()
    {
        NotEnoughMoneyObj.SetActive(true);
    }
    public void ExchangeDiamonds()
    {
        int diamondsToExchange = int.Parse(DiamondsInputField.text);

        if (diamondsToExchange <= Player.Diamonds)
        {
            Player.Coins += diamondsToExchange * 10;
            Player.Diamonds -= diamondsToExchange;
            Player.Save();
        }
        else
        {
            NotEnoughMoneyText();
        }
    }
}
