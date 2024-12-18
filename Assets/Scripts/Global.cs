using Unity.VisualScripting;
using UnityEngine;


public class Global : MonoBehaviour
{
    public class Controller
    {
        public KeyCode jump = KeyCode.W;
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;
        public KeyCode crouch = KeyCode.S;
        public KeyCode sprint = KeyCode.LeftShift;

        public MouseButton attack = MouseButton.Left;
    }
    public Controller controller;
    public class Physics
    {
        public float friction = 2f;
        public float ground_multiply = 3.0f;
        public float sprintMultiplier = 1.25f;
    }
    public class Mechanic
    {
        public float iFrame = 0.5f;
    }
    public Mechanic mechanic;
    public Physics physics;
    private static Global instance;
    public static Global Instance { get { return instance; } }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            controller = new Controller();
            physics = new Physics();
            mechanic = new Mechanic();
        }
    }
    private void OnDestroy()
    {
        // Clear the instance if this instance is destroyed
        if (instance == this)
        {
            instance = null;
        }
    }
}
