using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTutorial : MonoBehaviour
{
    public EndGame end;
    public void OnTriggerEnter(Collider other)
    {
        end.WinTutorial();
    }
}
