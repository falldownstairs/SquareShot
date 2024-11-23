using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    public HealthScript healthScript;

    private float damage;

    void Start()
    {
        healthScript = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthScript>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Player") && HealthScript.Instance.iframesActive() == false)
       {
            healthScript.LoseHealth(damage);
            Destroy(gameObject);
       }
       else if(collision.gameObject.CompareTag("wall"))
       {
            Destroy(gameObject);
       }

    }
    public void setDamage(float d){damage = d;}
}
