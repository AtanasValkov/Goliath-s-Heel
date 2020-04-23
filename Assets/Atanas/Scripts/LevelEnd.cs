using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public EndGame end;
    public void OnTriggerEnter(Collider other)
    {
        end.NextScene();
    }
}
