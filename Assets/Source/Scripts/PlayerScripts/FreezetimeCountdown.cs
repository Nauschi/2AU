using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezetimeCountdown : MonoBehaviour
{
    public GameObject TextDisplay; 
    public float secondsLeft = Assets.Source.GameSettings.GameConstants.TRAP_FREEZETIME;
    public bool triggered = false;

    public void Start()
    {
        if (triggered)
        {
            StartCoroutine(TimerTake());
        }else
        {
            TextDisplay.SetActive(false);
        }
    }

    public void TriggerCountdown()
    {
        triggered = true;
        secondsLeft = Assets.Source.GameSettings.GameConstants.TRAP_FREEZETIME;

        TextDisplay.SetActive(true);
        TextDisplay.GetComponent<Text>().text =secondsLeft +" Seconds";
        
        Start();
    }

    IEnumerator TimerTake()
    {
        while (secondsLeft > 0) { 
        yield return new WaitForSeconds(1f);
        secondsLeft--;
        TextDisplay.GetComponent<Text>().text = secondsLeft + " Seconds";
        }   
    }

}
