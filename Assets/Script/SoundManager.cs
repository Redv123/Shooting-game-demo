using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        Sound.OnSound += PlaySound;
    }

    private void OnDisable()
    {
        Sound.OnSound -= PlaySound;
    }

    private void PlaySound(AudioClip input)
    {
        sound.PlayOneShot(input);
    }
}
