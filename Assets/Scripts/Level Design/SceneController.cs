using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance; //bu scripte her yerden kolay eriþim için ekleniyor
    
    [SerializeField] Timer timer;
    [SerializeField] FlowerCounter flowerCounter;
    [SerializeField] LevelDisplayText levelDisplayText;

    GameObject[] flowers;
    public GameObject flowerPrefab;
    public int totalFlowers;
    int collectedFlowers;

    void Start()
    {
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        totalFlowers = flowers.Length;
        collectedFlowers = 0;

        ShowLevelAtStart();
    }

    void Update()
    {
        if (timer.remainingTime <= 1)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void CollectFlower()
    {
        collectedFlowers++;
        flowerCounter.FlowerCollected();

        if (collectedFlowers >= totalFlowers)
        {
            NextScene();
        }
    }

    void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
    }

    void ShowLevelAtStart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex >= 2)
        {
            int levelToDisplay = (currentSceneIndex - 2) + 1; //2 - 2 + 1 = 1 yani level 1
            levelDisplayText.gameObject.SetActive(true);
            levelDisplayText.ShowLevelText(levelToDisplay);
        }
    }
}
