using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveModel : MonoBehaviour
{
    public GameObject modelToRemove;

    void Start()
    {
        modelToRemove.SetActive(false);
    }
}
