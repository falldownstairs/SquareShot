
using System.Collections.Generic;
using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private Rigidbody2D rb;
    private Room associatedRoom;
    private GameObject player;
    private Vector3 dir;
    private Gun gun;

    private float pursueDistance;
    private float shootRange;

    private float health = 50f;

    [SerializeField] private SpriteRenderer SR;

    [SerializeField] private GameObject miniMapDisplay;

    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject sniper;

    [SerializeField] GameObject healthGain;

    Dictionary<Gun,GameObject> dict = new Dictionary<Gun, GameObject>();

    private Color color;

    private Vector3 scale;
    float speed = 4f;
    void Start()
    {
        dict.Add(GunContainer.badPistol,pistol);
        dict.Add(GunContainer.badShotgun,shotgun);
        dict.Add(GunContainer.badSniper,sniper);

        player = GameObject.FindGameObjectWithTag("Player");
        int rand = Random.Range(0,5);

        if (rand == 2)
        {
            gun = GunContainer.badShotgun;
            color = new Color(1f, 0.40f, 0.17f, 1);
            miniMapDisplay.GetComponent<SpriteRenderer>().color = color;
            SR.color = color;
            health = 100f;

            scale = new Vector3(1.75f,1.75f);
            transform.localScale = scale;
            pursueDistance = 8f;
            shootRange = 9f;
        }
        else if (rand == 3)
        {
            health = 30f;
            gun = GunContainer.badSniper;
            color = new Color(0.98f, 0.26f, 0.44f);
            SR.color = color;
            miniMapDisplay.GetComponent<SpriteRenderer>().color = color;
            scale = new Vector3(0.75f,0.75f);
            transform.localScale = scale;
            pursueDistance = 20f;
            shootRange = 23f;
        }
        else
        {
            color = GetComponent<SpriteRenderer>().color;
            gun = GunContainer.badPistol;
            scale = new Vector3(1f,1f);
            transform.localScale = scale;
            pursueDistance = 12f;
            shootRange = 12.5f;
        }
    }
    void Update()
    {
        if (health <=0)
        {
            int rand = Random.Range(0,21);
            GameObject effect = Instantiate(deathEffect,transform.position,Quaternion.identity);
                        Gradient grad = new Gradient();

            grad.SetKeys( new GradientColorKey[] 
            { new GradientColorKey(color, 0.0f), 
            new GradientColorKey(new Color(1f, 1f, 1f, 0), 1.0f) }, 
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
            new GradientAlphaKey(0.0f, 1.5f) } );

            var col = effect.GetComponent<ParticleSystem>().colorOverLifetime;
            col.enabled = true;
            col.color = grad;
            effect.transform.localScale = scale;
            associatedRoom.enemyDeath();

            if ((rand == 1 || rand == 2) && gun != GunContainer.badPistol){Instantiate(dict[gun],gameObject.transform.position,Quaternion.identity);}
            else if (rand == 3 || rand == 4){Instantiate(healthGain,gameObject.transform.position,Quaternion.identity);}
            Destroy(gameObject);
            Destroy(effect,3f);
            FindAnyObjectByType<AudioManager>().Play("enemyDeath");
        }
        
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer <= pursueDistance)
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

    public void LoseHealth(float amt){ health -= amt;}
    public void setRoom(Room room){associatedRoom = room;}

    public Gun getGun(){return gun;}

    public float getShootRange(){return shootRange;}

    public SpriteRenderer getSR(){return SR;}



}
