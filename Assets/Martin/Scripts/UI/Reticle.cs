using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    public Image dot;

    public void CanHitSomething(bool target)
    {
        dot.gameObject.SetActive(target);
    }
}
