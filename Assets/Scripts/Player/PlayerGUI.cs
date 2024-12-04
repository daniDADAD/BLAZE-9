using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Debug.Log(player.transform.position);
        }
    }
}
