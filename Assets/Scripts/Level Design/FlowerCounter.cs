using TMPro;
using UnityEngine;

public class FlowerCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI flowerCounterText;
    int totalFlowers;
    int remainingFlowers;

    SceneController sceneController;

    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        totalFlowers = sceneController.totalFlowers;
        remainingFlowers = totalFlowers;
        UpdateFlowerCounter();
    }

    public void FlowerCollected()
    {
        remainingFlowers--;
        UpdateFlowerCounter();
    }

    void UpdateFlowerCounter()
    {
        flowerCounterText.text = remainingFlowers.ToString();
    }
}
