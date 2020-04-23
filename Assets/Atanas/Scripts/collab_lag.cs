using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collab_lag : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        UnityEditor.EditorPrefs.SetBool("DeveloperMode", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Camera controlls
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
