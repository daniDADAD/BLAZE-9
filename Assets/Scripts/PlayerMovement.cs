using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rb;
    public Animator animator;
    public float jumpForce;
    public float speed;
    public float acceleration;
    public float friction;
    private float currentSpeed = 0.0f;
    private bool grounded;
    private Controller controller;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = Controller.Instance;
        Debug.Log(controller.jump);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speedCap = speed;

        if (Input.GetKey(controller.sprint))
        {
            speedCap = speed * 2;
            
        }
        if (Input.GetKey(controller.left))
        {

            currentSpeed = -speedCap;
            transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        if (Input.GetKey(controller.right))
        {

            currentSpeed = speedCap;
            transform.localRotation = Quaternion.Euler(0, 0, 0);


        }
        Friction();
        animator.SetFloat("Speed", Mathf.Abs(currentSpeed));
        rb.linearVelocityX = currentSpeed;
        if (Input.GetKey(controller.jump))
        {
            if (grounded)
            {
                rb.linearVelocityY = jumpForce;
            }


        }

    }
    private void Friction()
    {
        if (currentSpeed == 0)
            return;
        bool isNegative = currentSpeed < 0;
        float reduceSpeed = (isNegative) ? -friction : friction;
        if (!grounded)
        {
            reduceSpeed /= 2;
        }
        Debug.Log(Math.Abs(currentSpeed - reduceSpeed * Time.deltaTime));
        if (Math.Abs(currentSpeed - reduceSpeed * Time.deltaTime) <= 0.1f)
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed -= reduceSpeed * Time.deltaTime;
        }
    }
    private enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right };
    private HitDirection GetDirection(GameObject ObjectHit)
    {

        HitDirection hitDirection = HitDirection.None;
        RaycastHit MyRayHit;
        Vector3 direction = (gameObject.transform.position - ObjectHit.transform.position).normalized;
        Ray MyRay = new Ray(ObjectHit.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {

            if (MyRayHit.collider != null)
            {

                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (MyNormal == MyRayHit.transform.up) { hitDirection = HitDirection.Top; }
                if (MyNormal == -MyRayHit.transform.up) { hitDirection = HitDirection.Bottom; }
                if (MyNormal == MyRayHit.transform.forward) { hitDirection = HitDirection.Forward; }
                if (MyNormal == -MyRayHit.transform.forward) { hitDirection = HitDirection.Back; }
                if (MyNormal == MyRayHit.transform.right) { hitDirection = HitDirection.Right; }
                if (MyNormal == -MyRayHit.transform.right) { hitDirection = HitDirection.Left; }
            }
        }
        return hitDirection;
    }
    void OnCollisionEnter2D(Collision2D collision)

    {
        Debug.Log(GetDirection(collision.collider.gameObject));
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
