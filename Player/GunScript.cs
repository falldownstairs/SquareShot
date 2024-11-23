using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform spawner;
    private Transform player;
    public GameObject bullet;
    private bool canFire;
    private Camera mainCam;
    private Vector3 mousePos;
    public Gun gun;
    private float bulletlifespan = 3f;

    public static GunScript Instance {get; private set;}

    int bulletMultiplier = 1;

    float firerateMultiplier = 1f;
    void Start()
    {
        canFire = true;
        gun = GunContainer.pistol;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
    }
    void Update()
    {
        if (Time.deltaTime == 0f)
        {
            return;
        }
        if(canFire == true && Input.GetButton("Fire1"))
        {
            StartCoroutine(shootGun());
        }
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rot = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rot.y,rot.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ);
        
    }

    public void changeMultipliers(int _bulletMultiplier, float _firerateMultiplier)
    {
        bulletMultiplier += _bulletMultiplier;
        firerateMultiplier -= _firerateMultiplier;
    }
    void fireBullet(float spread = 0)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        GameObject newBullet = Instantiate(bullet,spawner.position,Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 bulletDirection = mousePos - player.position;
        bulletDirection += Vector2.Perpendicular(bulletDirection) * UnityEngine.Random.Range(-spread,spread);
        bulletRb.velocity = new Vector2(bulletDirection.x,bulletDirection.y).normalized * 35f * gun.bulletSpeed;
        newBullet.GetComponent<BulletScript>().setDamage(gun.damage);
        FindAnyObjectByType<AudioManager>().Play("shoot",gun.pitch,gun.volume);
        Destroy(newBullet,bulletlifespan);
    }
    private IEnumerator shootGun()
    {
        canFire = false;
        if (StaminaScript.Instance.CanUseStamina())
        {
            for(int i = 0; i<gun.bullets*bulletMultiplier; i++)
        {
            fireBullet(gun.spread);
        }
        StaminaScript.Instance.setRechargeTimer();
        StaminaScript.Instance.LoseStamina(gun.staminaDrain);
        }
        yield return new WaitForSeconds(gun.fireRate*firerateMultiplier);
        canFire = true;
    


    }
    public void setGun(Gun _gun){gun = _gun;}

    public Gun getGun(){
        return gun;
    }
}
