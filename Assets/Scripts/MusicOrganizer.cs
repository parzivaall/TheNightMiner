using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicOrganizer : MonoBehaviour
{
    public AudioSource musicSource;
    public List<AudioClip> musicClips;
    private Dictionary<string, AudioClip> sceneMusicTracks;

    void Start()
    {
        sceneMusicTracks = new Dictionary<string, AudioClip>();

        // Map scenes to corresponding music clips
        sceneMusicTracks.Add("MainMenu", musicClips[0]);
        sceneMusicTracks.Add("Shop", musicClips[1]);
        sceneMusicTracks.Add("Cave", musicClips[2]);

    }

    // public void PlaySceneMusic(string sceneName)
    // {
    //     AudioClip trackToPlay = null;

    //     switch (sceneName)
    //     {
    //         case "MainMenu":
    //             trackToPlay = sceneMusicTracks[0]; // Assign the corresponding track index
    //             break;
    //         case "Shop":
    //             trackToPlay = sceneMusicTracks[1];
    //             break;
    //         case "Cave":
    //             trackToPlay = sceneMusicTracks[2];
    //             break;
    //         default:
    //             trackToPlay = sceneMusicTracks[0]; // Default to first track if none specified
    //             break;
    //     }
    //     if (musicSource.clip != trackToPlay)
    //     {
    //         StartCoroutine(CrossfadeToNewTrack(trackToPlay));
    //     }
    // }

    // System.Collections.IEnumerator CrossfadeToNewTrack(AudioClip newTrack)
    // {
    //     // Fade out current track
    //     for (float volume = musicSource.volume; volume > 0; volume -= Time.deltaTime)
    //     {
    //         musicSource.volume = volume;
    //         yield return null;
    //     }

    //     // Switch tracks and fade in the new track
    //     musicSource.clip = newTrack;
    //     musicSource.Play();

    //     for (float volume = 0; volume < 1f; volume += Time.deltaTime)
    //     {
    //         musicSource.volume = volume;
    //         yield return null;
    //     }
    // }
}
