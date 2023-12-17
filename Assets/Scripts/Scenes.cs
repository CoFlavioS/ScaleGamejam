using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenes : MonoBehaviour  
{
    [SerializeField]
    private Image fade;


    public AudioClip soundClip; // Clip de sonido a reproducir

    private AudioSource audioSource;
    private Color black;
    void Start()
    {
        fade.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        black = fade.color;
    }

    public void PlaySound()
    {
        if (soundClip != null)
        {
            audioSource.PlayOneShot(soundClip); // Reproduce el sonido una vez al presionar el botón
        }
    }

    public IEnumerator Play()
    {
        fade.gameObject.SetActive(true);

        for (float alpha = 0f; alpha <= 1.1f; alpha += 0.1f)
        {
            black.a = alpha;
            fade.color = black;
            yield return new WaitForSeconds(.1f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayScene()
    {
        PlaySound();
        StartCoroutine(Play());
    }


    public IEnumerator Exit()
    {
        fade.gameObject.SetActive(true);

        for (float alpha = 0f; alpha <= 1.1f; alpha += 0.1f)
        {
            black.a = alpha;
            fade.color = black;
            yield return new WaitForSeconds(.1f);
        }
        Application.Quit();
    }
    public void ExitScene()
    {
        PlaySound();
        StartCoroutine(Exit());
    }

}
