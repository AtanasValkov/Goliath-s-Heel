using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winner : MonoBehaviour
{
    public Text whereToPlaceText;

    void Start()
    {
        float time = PlayerPrefs.GetFloat("time");

        whereToPlaceText.text = time.ToString("F1");
    }
}
