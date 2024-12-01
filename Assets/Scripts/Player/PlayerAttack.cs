using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Controller controller;
    public Animator animator;
    public float attackCooldownSecond;
    float currentCooldownSecond = 0.0f;
    int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        controller = Controller.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if (currentCooldownSecond > 0)
        {
            currentCooldownSecond -= Time.deltaTime;
        }
    }
    private void Attack()
    {
        
        if (!Input.GetMouseButtonDown((int)controller.attack) || currentCooldownSecond > 0)
        {
            return;
        }
        Debug.Log("attacking meow");
        currentCooldownSecond = attackCooldownSecond;
        
    }
}
