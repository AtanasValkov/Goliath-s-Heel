using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    float interval = 0.1f;

    bool doneFlashing;

    Renderer[] RendererArray;

    void Awake()
    {
        RendererArray = GetComponentsInChildren<Renderer>();
    }

    public void StartFlashing()
    {
        StartCoroutine(Flash());
    }

    public void StopFlashing()
    {
        doneFlashing = true;
    }

    IEnumerator Flash()
    {
        doneFlashing = false;

        while(!doneFlashing)
        {
            foreach(Renderer r in RendererArray) r.enabled = false;

            yield return new WaitForSeconds(interval);

            foreach(Renderer r in RendererArray) r.enabled = true;

            yield return new WaitForSeconds(interval);
        }
    }
}
