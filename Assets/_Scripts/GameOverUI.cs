using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        scoreText.text = $"Your Score: {finalScore}";
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
