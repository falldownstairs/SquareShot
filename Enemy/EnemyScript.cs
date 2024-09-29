
using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private Rigidbody2D rb;
    private Room associatedRoom;
    public Vector2 forceToApply;
    public float forceDamping = 1;
    private GameObject player;
    private Vector3 dir;
    private float health = 50f;
    float speed = 4f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (health <=0)
        {
            GameObject effect = Instantiate(deathEffect,transform.position,Quaternion.identity);
            associatedRoom.enemyDeath();
            Destroy(gameObject);
            Destroy(effect,3f);
        }
        
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer <= 10f)
        {
            rb.velocity = new Vector2(0f,0f);
            
        }
        else
        {
            dir = (player.transform.position - transform.position).normalized;
            Vector2 moveForce = dir * speed;
            rb.velocity = moveForce;
        }
    }
    public void LoseHealth(float amt)
    {
        health -= amt;
    }
    public void setRoom(Room room)
    {
        associatedRoom = room;
    }

}
