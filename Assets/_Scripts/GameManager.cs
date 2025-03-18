using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private LivesCounterUI livesCounter;
    [SerializeField] private GameObject gameOverScreen;


    private int currentBrickCount;
    private int totalBrickCount;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
        livesCounter.UpdateLives(maxLives);

    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        // add camera shake here
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        ScoreManager.scoreManager.AddScore(); //call scoremanager to add score! 
        if (currentBrickCount == 0)
        {
            SceneHandler.Instance.LoadNextScene();
            ScoreManager.scoreManager.InitializeScore(ScoreManager.scoreManager.GetScore());
        }
    }


    public void KillBall()
    {
        maxLives--;

        // Animate the lives decrement on HUD
        livesCounter.UpdateLives(maxLives);

        // Game over condition
        if (maxLives < 0)
        {
            StartCoroutine(GameOverSequence());
            return;
        }

        // Reset ball normally
        ball.ResetBall();
    }
    private IEnumerator GameOverSequence()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true); // activate your GameOver UI

        yield return new WaitForSecondsRealtime(1.5f); // use Realtime due to timeScale = 0

        Time.timeScale = 1f;
        SceneHandler.Instance.LoadMenuScene();
    }

}
