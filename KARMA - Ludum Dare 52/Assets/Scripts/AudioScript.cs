using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip introMusic;
    public AudioClip mainMusic;
    public AudioClip indianMusic;
    public AudioClip mariageMusic;
    private AudioSource audioSource;
    

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // Récupérer la source audio du GameObject
        audioSource = GetComponent<AudioSource>();

        // Configurer la source audio pour jouer l'intro au démarrage du jeu
        audioSource.clip = introMusic;
        audioSource.Play();
    }

    public void PlayMusic(int track)
    {

        switch(track)
        {
            case 1:
                audioSource.clip = mainMusic;
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = indianMusic;
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = mariageMusic;
                audioSource.Play();
                break;
        }
        // Mettre à jour la piste audio de la source audio pour jouer la musique principale
        
    }
}
