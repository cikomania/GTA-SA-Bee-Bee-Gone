using UnityEngine;

public class FlowerCollect : MonoBehaviour
{
    SceneController sceneController;
    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Score.score += 10;

        if (other.CompareTag("Player"))            
        {
            sceneController.CollectFlower();
            Destroy(gameObject);                
        }
            
        
    }
}
