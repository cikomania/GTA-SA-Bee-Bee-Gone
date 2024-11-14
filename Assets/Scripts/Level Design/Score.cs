using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score;
    [SerializeField] TextMeshProUGUI scoreText;
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
