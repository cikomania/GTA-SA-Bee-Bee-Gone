using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip background;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!musicSource.enabled)
        {
            musicSource.enabled = true;
        }

        if (scene.name == "Game Over")
        {
            musicSource.Stop();
        }
        else
        {
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
