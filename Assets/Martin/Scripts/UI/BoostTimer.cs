using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BoostTimer : MonoBehaviour
{
    public Image filledTimer;

    public UnityEvent boostTimerEnd;

    private Color green;

    void Awake()
    {
        green = new Color(0.06f, 0.7f, 0.06f);
        filledTimer.color = green;
    }

    public void boostTimerStart(float time)
    {
        StartCoroutine(Busy(time));
    }


    IEnumerator Busy(float totalTime)
    {
        float split = 0.1f;
        float rateOfFilling = split / totalTime;

        float filled = 0;       
        filledTimer.color = Color.red;
        
        while(filled < 1)
        {
            filledTimer.fillAmount = filled;

            yield return new WaitForSeconds(split);

            filled += rateOfFilling;

            if (filled > 1) filled = 1; // just a precaution
        }

        filledTimer.fillAmount = filled;
        filledTimer.color = green;
        boostTimerEnd.Invoke();
    }
}
