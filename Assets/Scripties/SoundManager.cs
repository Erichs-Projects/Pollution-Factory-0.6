using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Range(0.0f, 1.0f)]
    public float sfxVolume = 1.0f;

    [Range(0.0f, 1.0f)]
    public float bgmVolume = 1.0f;

    public List<SoundClipPair> sounds;
    private Dictionary<string, AudioClip> soundMap;

    public List<SoundClipPair> music;
    private Dictionary<string, AudioClip> musicMap;

    AudioSource musicSource;

    private void Awake()
    {
        // Makes sure only one instance exists
        if (instance != null)
        {
            Destroy(this);
        }
        else {
            // Makes sure the instance stays alive thru all scenes.
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Places sounds in a dictionary for quick access
        soundMap = new Dictionary<string, AudioClip>();
        foreach (var sound in sounds)
        {
            soundMap[sound.name] = sound.clip;
        }

        // Places music in a dictionary for quick access
        musicMap = new Dictionary<string, AudioClip>();
        foreach (var song in music)
        {
            musicMap[song.name] = song.clip;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.volume = bgmVolume;
        musicSource.loop = true;
        musicSource.playOnAwake = false;
    }

    public void PlaySound(string name) // Overloaded version in case the player doesn't care where the sound goes
    {
        PlaySound(name, Vector3.zero);
    }

    
    public void PlaySound(string name, Vector3 position)
    {
        if (soundMap.ContainsKey(name))
        {
            // Create a new gameobject to play the sound
            GameObject soundGameObject = new GameObject("Sound " + name);
            soundGameObject.transform.position = position;

            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = soundMap[name];
            audioSource.loop = false;
            audioSource.spatialBlend = 1.0f;
            audioSource.playOnAwake = false;
            audioSource.volume = sfxVolume;
            audioSource.Play();
        

           // Set the object to destroy itself after the sound's completion
            Destroy(soundGameObject, audioSource.clip.length);
        }
        else
        {
            Debug.Log(name + " is not a sound in the list");
        }
    }

    public void PlaySong(string name)
    {
        // Plays a song from the map
        if (musicMap.ContainsKey(name))
        {
            musicSource.clip = musicMap[name];
            musicSource.volume = bgmVolume;
            musicSource.Play();
        }
        else
        {
            Debug.Log(name + " is not a song in the list");
        }
    }

    public void StopMusic()
    {
        // Stops the music
        musicSource.Stop();
    }

    // Makes inputting sounds thru the inspector easier
    [System.Serializable]
    public class SoundClipPair
    {
        public string name;
        public AudioClip clip;
    }
}
