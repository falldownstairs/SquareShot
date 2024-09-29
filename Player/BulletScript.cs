using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Enemy"))
       {
            collision.gameObject.GetComponent<EnemyScript>().LoseHealth(10f);
            Destroy(gameObject);
       }
       else if(collision.gameObject.CompareTag("wall"))
       {
            Destroy(gameObject);
       }

    }
}
