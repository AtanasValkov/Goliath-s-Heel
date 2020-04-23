using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walldestruct : MonoBehaviour
{
    public List<GameObject> blocks;

    // Update is called once per frame
    void Update()
    {
     if(blocks.Count <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
