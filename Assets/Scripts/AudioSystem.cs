using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject turnOnButton;
    public GameObject turnOffButton;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            if (PlayerPrefs.GetString("Music") == "off")
            {
                TurnOffMusic();
                turnOnButton.SetActive(true);
                turnOffButton.SetActive(false);
            }
            else
            {
                TurnOnMusic();
                turnOnButton.SetActive(false);
                turnOffButton.SetActive(true);
            }
        }
        else
        {
            TurnOnMusic();
            turnOnButton.SetActive(false);
            turnOffButton.SetActive(true);
        }
        
    }
    public void TurnOffMusic()
    {
        audioSource.Pause();
        PlayerPrefs.SetString("Music", "off");
        PlayerPrefs.Save();
    }
    public void TurnOnMusic()
    {
        audioSource.Play();
        PlayerPrefs.SetString("Music", "on");
        PlayerPrefs.Save();
    }
}
