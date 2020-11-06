using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private bool musicPlaying = false;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();
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
}
