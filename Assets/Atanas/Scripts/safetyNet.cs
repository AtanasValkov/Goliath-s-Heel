using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safetyNet : MonoBehaviour
{
    public GameObject camera;
    public Vector3 respawn;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(respawn.x,respawn.y,respawn.z);
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        camera.transform.rotation = new Quaternion(0, 90f, 0, 0);
    }
}
