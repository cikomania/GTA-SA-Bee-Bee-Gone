using TMPro;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{    
    [SerializeField] TextMeshProUGUI livesCounterText;

    BeeController beeController;
    void Start()
    {
        beeController = FindObjectOfType<BeeController>();
        UpdateLivesCounter();
    }

    void Update()
    {
        if (beeController != null)
        {
            UpdateLivesCounter();
        }
    }

    void UpdateLivesCounter()
    {
        int displayedLives = Mathf.Max(0, beeController.lives);
        livesCounterText.text = beeController.lives.ToString();
    }
}
