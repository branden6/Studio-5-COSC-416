using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private LivesCounterUI livesCounter;
    [SerializeField] private ScoreCounterUI scoreCounter;
    [SerializeField] private GameObject explosionEffect;


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
        Instantiate(explosionEffect, position, Quaternion.identity);
        AudioManager.Instance.PlaySFX("ballHit");

        // add camera shake here
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        ScoreManager.scoreManager.AddScore(); //call scoremanager to add score! 
        if (currentBrickCount == 0)
        {
            SceneHandler.Instance.LoadNextScene();
             AudioManager.Instance.PlaySFX("levelPassed");
            ScoreManager.scoreManager.InitializeScore(ScoreManager.scoreManager.GetScore());
        }
    }


public void KillBall()
{
    maxLives--;
    livesCounter.UpdateLives(maxLives);
    if(maxLives >= 1){
        AudioManager.Instance.PlaySFX("lostLife");
    }
    

    if (maxLives <= 0)
    {
        PlayerPrefs.SetInt("FinalScore", ScoreManager.scoreManager.GetScore()); // ← move here!
        StartCoroutine(GameOverSequence());
        return;
    }

    ball.ResetBall();
}


    private IEnumerator GameOverSequence()
    {
        AudioManager.Instance.PlaySFX("gameOver2");
        yield return new WaitForSecondsRealtime(0.25f);
        SceneManager.LoadScene("GameOverScene");
    }


}
