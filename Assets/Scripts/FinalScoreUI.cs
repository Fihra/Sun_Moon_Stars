using UnityEngine;
using TMPro;

public class FinalScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lootScoreText;

    int finalScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalScore = ScoreManager.Instance.GetHomeScore();

        lootScoreText.text = finalScore.ToString();
    }

}
