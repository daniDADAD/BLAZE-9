using UnityEngine;

public class SpikeEntity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Entity entity;
    public EntityStats stats;
    void Start()
    {
        entity = gameObject.AddComponent<Entity>();
        entity.Initialize(stats);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)

    {

        // Check if the collided object has the "Collidable" tag
        Entity collidedEntity = collision.gameObject.GetComponent<Entity>();
        if (collidedEntity != null)
        {
            // collidedStats.Damage(stats.damage);
            collidedEntity.stats.Damage(entity.stats.damage);
        }
    }


    void OnCollisionExit2D(Collision2D collision)

    {

        // Check if the collided object has the "Collidable" tag



    }
}
