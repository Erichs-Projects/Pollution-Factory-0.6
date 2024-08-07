using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;


[RequireComponent(typeof(AudioSource))]
public class MonsterSoundEffects : MonoBehaviour
{
    public List<AudioClip> sounds;

    private void Start()
    {
        InvokeRepeating("PlayMonsterSound", 0.0f, 10.0f);
    }

    void PlayMonsterSound()
    {
        AudioClip nextPlay = sounds[Random.Range(0, sounds.Count)];
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = nextPlay;
        Debug.Log(audio.clip.name);
        audio.Play();
    }

}
