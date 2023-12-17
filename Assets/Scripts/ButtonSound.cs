using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Button button; // Referencia al botón en el Inspector
    public AudioClip soundClip; // Clip de sonido a reproducir

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Asigna el AudioSource si no está adjuntado al GameObject
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        button.onClick.AddListener(PlaySound); // Asigna la función PlaySound al evento onClick del botón
    }

    public void PlaySound()
    {
        if (soundClip != null)
        {
            audioSource.PlayOneShot(soundClip); // Reproduce el sonido una vez al presionar el botón
        }
    }
}