using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerGUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Player player;
    TextMeshProUGUI healthText;
    TextMeshProUGUI deathText;
    Button respawnButton;
    void Start()
    {
        player = FindObjectOfType<Player>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        deathText = GameObject.Find("DeathText").GetComponent<TextMeshProUGUI>();
        respawnButton = GameObject.Find("RespawnButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {



            return;
        }
        bool alive = player.animator.GetBool("Alive");
        healthText.gameObject.SetActive(alive);
        respawnButton.gameObject.SetActive(!alive);
        deathText.gameObject.SetActive(!alive);
        if (!alive)
        {
            respawnButton.onClick.AddListener(Respawn);
            return;
        }
        Debug.Log(healthText);

        healthText.text = "Health: " + player.getEntity().stats.currentHealth.ToString();
    }
    void Respawn()
    {
        player.Respawn();

    }
}
