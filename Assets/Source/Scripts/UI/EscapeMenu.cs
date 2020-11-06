using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{

    private bool musicPlaying = false;

    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
