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



    private float bulletSpeed = 25f;
    void Start()
    {
        canFire = true;
        gun = GunContainer.pistol;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
    void fireBullet(float spread = 0)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        GameObject newBullet = Instantiate(bullet,spawner.position,Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 bulletDirection = mousePos - player.position;
        bulletDirection += Vector2.Perpendicular(bulletDirection) * UnityEngine.Random.Range(-spread,spread);
        bulletRb.velocity = new Vector2(bulletDirection.x,bulletDirection.y).normalized * bulletSpeed;
        FindAnyObjectByType<AudioManager>().Play("shoot");
        Destroy(newBullet,bulletlifespan);
    }
        private IEnumerator shootGun()
    {
        canFire = false;
        for(int i = 0; i<gun.bullets; i++)
        {
            fireBullet(gun.spread);
        }
        yield return new WaitForSeconds(gun.fireRate);
        canFire = true;


    }
}
