using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{

    private bool musicPlaying = false;
    public bool IsMenuOpen = false;
    public GameObject SoundsMenu;
    public GameObject OptionsMenu;

    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        this.gameObject.SetActive(false);
        IsMenuOpen = false;
    }

    public void ResumeGame()
    {
        this.gameObject.SetActive(false);
        IsMenuOpen = false;
    }

    public void OnValueChanged(float value)
    {
        if (value > 0)
        {
            if (!musicPlaying)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioSource>().clip);
                musicPlaying = true;
            }
        }
    }

    public void OnClickSettingsButton()
    {
        if (IsMenuOpen)
        {
            this.gameObject.SetActive(false);
            OptionsMenu.SetActive(false);
            SoundsMenu.SetActive(false);
            IsMenuOpen = false;
        }
        else
        {
            this.gameObject.SetActive(true);
            IsMenuOpen = true;
        }
    }
}
