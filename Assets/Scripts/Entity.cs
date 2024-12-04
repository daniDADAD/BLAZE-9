using UnityEngine;
[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/Entity", order = 1)]
public class Entity : ScriptableObject
{
    public int health;
    public float movementSpeed;
    public string name;
    public int damage;
    public float jumpStrength;

}
