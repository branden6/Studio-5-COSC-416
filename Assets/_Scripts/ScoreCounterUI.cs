using UnityEngine;
using System.Collections;
using DG.Tweening;
using TMPro;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform scoreTextContainer;
    [SerializeField] private float duration;
    [SerializeField] private Ease animationCurve;

    private float containerInitPosition;
    private float moveAmount;

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        // Initialize the UI with the current score from ScoreManager
        int initialScore = ScoreManager.scoreManager.GetScore();
        current.SetText($"{initialScore}");
        toUpdate.SetText($"{initialScore}");
        containerInitPosition = scoreTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }

    public void UpdateScore(int score)
    {
        toUpdate.SetText($"{score}");
        scoreTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetScoreContainer(score));
    }

    private IEnumerator ResetScoreContainer(int score)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{score}");
        Vector3 localPosition = scoreTextContainer.localPosition;
        scoreTextContainer.localPosition = new Vector3(scoreTextContainer.localPosition.x, containerInitPosition, localPosition.z);
    }
}