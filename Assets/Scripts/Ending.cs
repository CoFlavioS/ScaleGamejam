using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private bool falso;
    public AudioClip soundClip;
    private AudioSource audioSource;

    // Start is called before the first frame update

    private void Start()
    {
        falso = false;
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlaySound()
    {
        if (soundClip != null)
        {
            audioSource.PlayOneShot(soundClip); // Reproduce el sonido una vez al presionar el botón
        }
    }

    // Update is called once per frame
    public void Menu()
    {
        PlaySound();
        StartCoroutine(Wait());
        SceneManager.LoadScene("Inicio");
    }

    public IEnumerator Wait()
    {
        if (!falso)
        {
            yield return new WaitForSeconds(1f);
        }
        falso = true;  
     
    }
}
