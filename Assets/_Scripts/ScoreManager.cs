using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreCounterUI scoreCounter;
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

    public void AddScore()
    {
        score += 1;  
        scoreCounter.UpdateScore(score);
    }

    public int GetScore()
    {
        return score;
    }

    public void InitializeScore(int startingScore)
    {
        score = startingScore;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreCounter.UpdateScore(score);
    }
    public void UpdateScoreCounterReference(ScoreCounterUI newScoreCounter)
{
    scoreCounter = newScoreCounter;
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