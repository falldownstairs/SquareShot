using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int healthExpand;
    public float healthGain;
    public int bulletMultiply;
    public float firerateMultiplier;
    public int staminaExpand;
    public bool tpToBoss;

    public bool pistol = false;
    public bool AR = false;
    public bool shotGun = false;
    public bool Sniper = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player"))
        {
        if (HealthScript.Instance.isMaxHealth() && healthGain > 0){}
        else{
            HealthScript.Instance.expandHealth(healthExpand);
            HealthScript.Instance.GainHealth(healthGain);
            StaminaScript.Instance.expandStamina(staminaExpand);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GunScript>().changeMultipliers(bulletMultiply,firerateMultiplier);

            if (pistol){GunScript.Instance.setGun(GunContainer.pistol);}
            else if (AR){GunScript.Instance.setGun(GunContainer.AR);}
            else if (shotGun){GunScript.Instance.setGun(GunContainer.shotgun);}
            else if (Sniper){GunScript.Instance.setGun(GunContainer.sniper);}
            else {}
            FindAnyObjectByType<AudioManager>().Play("powerUp");
            Destroy(gameObject);
            }
        if(tpToBoss){
            GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = GameObject.FindGameObjectWithTag("BossRoom").gameObject.transform.position;
        }


       }
    }

    // Update is called once per frame

}
