using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;  
    private int score = 0;

    public static ScoreManager scoreManager { get; private set; }

    private void Awake()
    {
        if (scoreManager == null)
        {
            scoreManager = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore()
    {
        score += 1;  
        UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScoreTextReference(TMP_Text newScoreText)
    {
        scoreText = newScoreText;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";  
        }
    }

    public void InitializeScore(int startingScore)
    {
        score = startingScore;
        UpdateScoreUI();
    }
    public void DestroyScoreManager()
    {
        if (scoreManager == this)
        {
            Destroy(gameObject);
        }
    }
}