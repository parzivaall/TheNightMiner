using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvManager : MonoBehaviour
{
    public static EnvManager Instance;
    private int score = 0;
    private int maxHealth = 100;
    private int health;
    public AudioSource musicSource;
    public AudioClip[] sceneMusicTracks; // Assign different music clips for each scene
    private string currentSceneName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        currentSceneName = SceneManager.GetActiveScene().name;

        // Play the music for the initial scene
        PlaySceneMusic(currentSceneName);

        // Subscribe to scene change event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void start(){
        maxHealth = maxHealth;
    }

    void OnDestroy()
    {
        // Unsubscribe from the scene change event when destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the scene has changed
        if (scene.name != currentSceneName)
        {
            // Update current scene name
            currentSceneName = scene.name;

            // Play the appropriate music for the new scene
            PlaySceneMusic(currentSceneName);
        }
    }

    void PlaySceneMusic(string sceneName)
    {
        // Select the appropriate music track for the scene
        AudioClip trackToPlay = null;

        switch (sceneName)
        {
            case "MainMenu":
                trackToPlay = sceneMusicTracks[0]; // Assign the corresponding track index
                break;
            case "Shop":
                trackToPlay = sceneMusicTracks[1];
                break;
            case "Cave":
                trackToPlay = sceneMusicTracks[2];
                break;
            default:
                trackToPlay = sceneMusicTracks[0]; // Default to first track if none specified
                break;
        }

        // Only play if it's a new track
        if (musicSource.clip != trackToPlay)
        {
            StartCoroutine(CrossfadeToNewTrack(trackToPlay));
        }
    }

    System.Collections.IEnumerator CrossfadeToNewTrack(AudioClip newTrack)
    {
        // Fade out current track
        for (float volume = musicSource.volume; volume > 0; volume -= Time.deltaTime)
        {
            musicSource.volume = volume;
            yield return null;
        }

        // Switch tracks and fade in the new track
        musicSource.clip = newTrack;
        musicSource.Play();

        for (float volume = 0; volume < 1f; volume += Time.deltaTime)
        {
            musicSource.volume = volume;
            yield return null;
        }
    }

    public void setHealth(int damage)
    {
        health += damage;
        if (health <= 0) { SceneManager.LoadScene(3); }
        else if (health > 100) { health = 100; }
    }

    public int getScore(){
        return score;
    }
    public void setScore(int amount)
    {
        score = amount;
    }

    public void addScore(int amount)
    {
        score += amount;
    }

    public int getHealth()
    {
        return health;
    }

    public void LoadScene(int level){

        SceneManager.LoadScene(level);
    }
}
