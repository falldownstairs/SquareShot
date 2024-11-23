using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody2D rb;

    private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Enemy"))
       {
          collision.gameObject.GetComponent<EnemyScript>().LoseHealth(damage);
          Destroy(gameObject);
       }
       if(collision.gameObject.CompareTag("wall"))
       {
          Destroy(gameObject);
       }
       else if(collision.gameObject.CompareTag("boss"))
       {
          BossHealthScript.Instance.LoseHealth(damage);
          Destroy(gameObject);
       }

    }
    public void setDamage(float d){damage = d;}
}
