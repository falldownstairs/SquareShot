using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    public HealthScript healthScript;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthScript>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.name == "player")
       {
            healthScript.LoseHealth(10f);
            Destroy(gameObject);
       }
       else if(collision.gameObject.CompareTag("wall"))
       {
            Destroy(gameObject);
       }

    }
}
