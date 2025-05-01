using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource sfx;

    //public List<AudioClip> musics;
    public List<AudioClip> sounds;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // Nếu đã có instance khác rồi thì hủy bản duplicate
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PLaySound(SoundFx soundFx)
    {
        sfx.PlayOneShot(sounds[(int)soundFx]);
    }
    public enum SoundFx {
        sword,
        staff,
        bow,
        hit
    }

    //AudioManager.instance.PLaySound(AudioManager.SoundFx.weapon);
}
