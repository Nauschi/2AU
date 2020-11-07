using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class FreezetimeCountdown : MonoBehaviour
{
    public GameObject textDisplay; 
    public float secondsLeft = Assets.Source.GameSettings.GameConstants.TRAP_FREEZETIME;
    public bool triggered = false;

    public void Start()
    {
        if (triggered)
        {
            StartCoroutine(TimerTake());
        }else
        {
            //GameObject pane = gameObject.GetComponentInParent<Canvas>().gameObject;
            //gameObject.Fi
            
            textDisplay.SetActive(false);
        }
    }

    public void TriggerCountdown()
    {
        triggered = true;
        secondsLeft = Assets.Source.GameSettings.GameConstants.TRAP_FREEZETIME;

        textDisplay.SetActive(true);
        textDisplay.GetComponent<Text>().text =secondsLeft +" Seconds";
        
        Start();
    }

    IEnumerator TimerTake()
    {
        while (secondsLeft > 0) { 
        yield return new WaitForSeconds(1f);
        secondsLeft--;
        textDisplay.GetComponent<Text>().text = secondsLeft + " Seconds";
        }   
    }

}
