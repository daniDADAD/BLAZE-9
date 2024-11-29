using System.Diagnostics;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject camera;
    void Start()
    {
        camera = GameObject.Find("PlayerCamera");
    }
    enum CameraMode { Default };
    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
