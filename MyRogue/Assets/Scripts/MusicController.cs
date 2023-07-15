using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;

    public static MusicController Instance
    {
        get { return instance; }
    }

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip musicClip)
    {
        audioSource.clip = musicClip;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
