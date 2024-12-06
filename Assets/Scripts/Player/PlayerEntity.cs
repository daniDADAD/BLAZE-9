using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Entity entity;
    public Entity getEntity()
    {
        return this.entity;
    }
    public EntityStats stats;
    Global global;
    public Animator animator;
    float M1CooldownTime = 0.0f;
    private float M1CurrentTime = 0.0f;


    Rigidbody2D rb;
    private float currentSpeed = 0.0f;
    private bool grounded;
    private int prevHealth;

    AnimatorStateInfo animStateInfo;
    int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        global = Global.Instance;
        M1CooldownTime = AnimationLength("Attack");
        rb = GetComponent<Rigidbody2D>();
        entity = gameObject.AddComponent<Entity>();
        entity.Initialize(stats);
        prevHealth = entity.stats.currentHealth;
    }
    void Awake()
    {

    }
    public void Respawn()
    {
        animator.SetBool("Alive", true);
        transform.position = new Vector3(0, 0, 0);
        rb.linearVelocity = new Vector2(0, 0);
        entity.stats.Initialize();
        // do everything else basically meow meow meow meow meow
    }
    float AnimationLength(string name)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name == name)
                time = ac.animationClips[i].length;
        return time;
    }


    // Update is called once per frame
    void Update()
    {
        bool alive = animator.GetBool("Alive");
        if (!alive)
        {

            return;
        }
        if (entity.stats.currentHealth <= 0)
        {
            Die();
        }
        if (Input.GetMouseButtonDown((int)global.controller.attack) &&

        !animator.GetBool("isAttacking")
        && !animator.GetBool("Jumping"))
        {
            Attack();
            animator.SetBool("isAttacking", true);
            M1CurrentTime = M1CooldownTime;
        }
        if (FinishCooldown())
        {
            animator.SetBool("isAttacking", false);
        }
        if (entity.stats.currentHealth < prevHealth)
        {
            Damage();
        }
        prevHealth = entity.stats.currentHealth;
        Move();

    }
    public bool FinishCooldown()
    {
        M1CurrentTime -= Time.deltaTime;
        return M1CurrentTime < 0.0f;
    }
    public void Die()
    {
        animator.SetBool("Alive", false);
        rb.linearVelocityX = 0;
    }
    private void Attack()
    {


        Debug.Log("attacking meow");


    }
    private void Damage()
    {
        entity.stats.isInvincible = true;
        StartCoroutine(entity.stats.invincibleCooldown(Global.Instance.mechanic.iFrame));
        Debug.Log("You are damaged meow");
    }
    private void Move()
    {
        float speedCap = entity.stats.movementSpeed;

        if (Input.GetKey(global.controller.sprint))
        {
            speedCap = entity.stats.movementSpeed * 1.25f;

        }

        if (Input.GetKey(global.controller.left))
        {

            currentSpeed = -speedCap;
            transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        if (Input.GetKey(global.controller.right))
        {

            currentSpeed = speedCap;
            transform.localRotation = Quaternion.Euler(0, 0, 0);


        }
        Friction();
        animator.SetFloat("Speed", Mathf.Abs(currentSpeed));
        rb.linearVelocityX = currentSpeed;
        if (Input.GetKey(global.controller.jump))
        {
            if (grounded)
            {
                rb.linearVelocityY = entity.stats.jumpStrength;
            }


        }
    }
    private void Friction()
    {
        if (currentSpeed == 0)
            return;
        bool isNegative = currentSpeed < 0;
        float friction = global.physics.friction;
        if (grounded)
        {
            friction *= global.physics.ground_multiply;
        }
        float reduceSpeed = (isNegative) ? -friction : friction;
        if (!grounded)
        {
            reduceSpeed /= 2;
        }
        //  Debug.Log(Math.Abs(currentSpeed - reduceSpeed * Time.deltaTime));
        if (Math.Abs(currentSpeed - reduceSpeed * Time.deltaTime) <= 0.1f)
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed -= reduceSpeed * Time.deltaTime;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)

    {

        // Check if the collided object has the "Collidable" tag

        if (collision.collider.CompareTag("Collidable"))

        {
            currentSpeed = 0.0f;
            grounded = true;
            animator.SetBool("Jumping", !grounded);
        }
    }


    void OnCollisionExit2D(Collision2D collision)

    {

        // Check if the collided object has the "Collidable" tag

        if (collision.collider.CompareTag("Collidable"))

        {
            grounded = false;
            animator.SetBool("Jumping", !grounded);
        }

    }
}
