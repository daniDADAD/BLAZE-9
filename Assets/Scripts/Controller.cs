using Unity.VisualScripting;
using UnityEngine;


public class Controller : MonoBehaviour
{
    public KeyCode jump = KeyCode.W;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode crouch = KeyCode.S;
    public KeyCode sprint = KeyCode.LeftShift;
   
    public MouseButton attack = MouseButton.Left;
    private static Controller instance;
    public static Controller Instance { get { return instance; } }
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
