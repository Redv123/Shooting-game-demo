using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.loop = true;
    }

    public void StopMusic()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (audioSource.clip != clip)
        {
            StopMusic();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

}
