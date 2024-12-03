using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Controller controller;
    public Animator animator;
    public float M1CooldownTime = 0.0f;
    private float M1CurrentTime = 0.0f;
    AnimatorStateInfo animStateInfo;
    int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        controller = Controller.Instance;
        M1CooldownTime = AnimationLength("Attack");
    }
    float AnimationLength(string name)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name == name)
                time = ac.animationClips[i].length;
        Debug.Log(time);
        return time;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown((int)controller.attack) && !animator.GetBool("isAttacking")
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

    }
    public bool FinishCooldown()
    {
        M1CurrentTime -= Time.deltaTime;
        return M1CurrentTime < 0.0f;
    }

    private void Attack()
    {


        Debug.Log("attacking meow");


    }
}
