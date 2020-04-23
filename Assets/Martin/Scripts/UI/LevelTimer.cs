using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelTimer : MonoBehaviour
{
    public Image filledTimer;
    public float totalTimeSec;
    private IEnumerator timer;
    public UnityEvent levelEnded;

    private float filled;

    void Start()
    {
        timer = LevelInProgress();
        StartCoroutine(timer);
    }

    IEnumerator LevelInProgress()
    {
        filled = 1;

        float split = 0.1f;
        float rateOfFilling = split / totalTimeSec;
        
        while(filled > 0)
        {
            filledTimer.fillAmount = filled;

            yield return new WaitForSeconds(split);

            filled -= rateOfFilling;

            if (filled < 0) filled = 0; // just a precaution
        }

        filledTimer.fillAmount = filled;
        levelEnded.Invoke();
    }

    public float StopTimer()
    {
        StopCoroutine(timer);

        float timeLeft = filled * totalTimeSec;

        float timePassed = totalTimeSec - timeLeft;

        return timePassed;
    }
}
