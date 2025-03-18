using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneHandler : SingletonMonoBehavior<SceneHandler>
{
    [Header("Scene Data")]
    [SerializeField] private List<string> levels;
    [SerializeField] private string menuScene;
    [Header("Transition Animation Data")]
    [SerializeField] private Ease animationType;
    [SerializeField] private float animationDuration;
    [SerializeField] private RectTransform transitionCanvas;

    private int nextLevelIndex;
    private float initXPosition;

    protected override void Awake()
    {
        base.Awake();
        initXPosition = transitionCanvas.transform.localPosition.x;
        SceneManager.LoadScene(menuScene);
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode _)
    {
        // Update ScoreCounterUI reference when loading a new level
        if (scene.name != menuScene)
        {
            ScoreCounterUI scoreCounterUI = FindObjectOfType<ScoreCounterUI>();
            if (scoreCounterUI != null)
            {
                ScoreManager.scoreManager.UpdateScoreCounterReference(scoreCounterUI);
                ScoreManager.scoreManager.InitializeScore(ScoreManager.scoreManager.GetScore());
            }
        }

        transitionCanvas.DOLocalMoveX(initXPosition, animationDuration).SetEase(animationType);
    }

    public void LoadNextScene()
    {
        if (nextLevelIndex >= levels.Count)
        {
            ScoreManager.scoreManager.DestroyScoreManager();
            LoadMenuScene();
        }
        else
        {
            transitionCanvas.DOLocalMoveX(initXPosition + transitionCanvas.rect.width, animationDuration).SetEase(animationType);
            StartCoroutine(LoadSceneAfterTransition(levels[nextLevelIndex]));
            nextLevelIndex++;
        }
    }

    public void LoadMenuScene()
    {
        transitionCanvas.DOLocalMoveX(initXPosition + transitionCanvas.rect.width, animationDuration).SetEase(animationType);
        StartCoroutine(LoadSceneAfterTransition(menuScene));
        nextLevelIndex = 0;
    }

    private IEnumerator LoadSceneAfterTransition(string scene)
    {
        yield return new WaitForSeconds(animationDuration);
        SceneManager.LoadScene(scene);
    }
}