using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTo : MonoBehaviour
{
    public Vector3 location;
    
    void Start()
    {
        transform.position = location;
    }
}
