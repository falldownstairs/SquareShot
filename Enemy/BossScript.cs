using System;
using Unity.Mathematics;
using UnityEngine;
public class BossScript : MonoBehaviour
{

    [SerializeField] GameObject bullet;

    [SerializeField] GameObject pivotPoint;

    [SerializeField] GameObject mainCannon;

    [SerializeField] Rigidbody2D RB;

    [SerializeField] GameObject deathEffect;
    private Color color;

    private Vector3 scale;

    private GameObject[] minicannons;

    private float timer;

    private float reversetimer;

    private float modeTimer;
    private float angle;

    private float coefficent;

    private string atkMode;
    string[] atkArray = new string[3];

    private int atkCounter = 2;
    private float delayTimer = 0f;

    private float soundTimer = 0f;

    void Start()
    {
        FillArray();
        DebugArrayContents();
        minicannons = GameObject.FindGameObjectsWithTag("MiniCannon");
        timer = 1f;
        coefficent = -1f;
        atkMode = "atk0";
    }
    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer -= Time.deltaTime;
        reversetimer -= Time.deltaTime;
        modeTimer -= Time.deltaTime;
        delayTimer -= Time.deltaTime;
        soundTimer -= Time.deltaTime;

        if (atkCounter == 2){
            FillArray();
            DebugArrayContents();
            atkCounter = 0;
        }
        if(modeTimer <=0){
            atkMode = atkArray[atkCounter];
            atkCounter++;
            if (atkMode == "atk0"){
                delayTimer = 1.5f;
                modeTimer = 3.5f;
                for(int i = 0; i<8; i++){
                    minicannons[i].SetActive(true);
                    
                }
                mainCannon.SetActive(false);
                }
            else if (atkMode == "atk1"){
                delayTimer = 0.5f;
                modeTimer = 1.2f;
                for(int i = 0; i<8; i++){
                    minicannons[i].SetActive(false);
                    
                }
                mainCannon.SetActive(true);
                }
            else {
                delayTimer = 1.0f;
                modeTimer = 6f;
                for(int i = 0; i<8; i++){
                    minicannons[i].SetActive(false);
                    
                }
                mainCannon.SetActive(true);}
            pivotPoint.transform.rotation = Quaternion.Euler(0,0,0);
            pivotPoint.transform.rotation = Quaternion.Euler(0,0,0);
            RB.velocity = new Vector2(0,0);
        }
        
        if(atkMode == "atk0"){
            if (timer <=0 && delayTimer <= 0){
                shootMiniCannons();
                timer = 0.3f;
            }
            if(soundTimer <=0 && delayTimer <= 0){
                FindAnyObjectByType<AudioManager>().Play("shoot",GunContainer.pistol.pitch,GunContainer.pistol.volume);
                soundTimer = 0.15f;
            }
            if (reversetimer <= 0 && delayTimer <= 0){
                coefficent *= -1f;
                reversetimer = 1.5f;
                shootCircle();
            }
            angle += Time.deltaTime*30f*-1; 
            pivotPoint.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        
        if(atkMode == "atk1"){
            if (timer <=0 && delayTimer <= 0){
                shootShotgun(player);
                timer = 0.2f;
            }
            Vector3 rot = player.position - transform.position;
            float rotZ = Mathf.Atan2(rot.y,rot.x) * Mathf.Rad2Deg;
            pivotPoint.transform.rotation = Quaternion.Euler(0,0,rotZ);

            float distanceToPlayer = (player.position - transform.position).magnitude;
            if (distanceToPlayer >= 15f){
                Vector3 dir = player.position - transform.position;
                RB.velocity = new Vector2(dir.x,dir.y).normalized*4f;
            }
            else{
                RB.velocity = new Vector2(0,0);
            }
        }
        if(atkMode == "atk2"){
            if (timer <=0 && delayTimer <= 0){
                shootMainCannon(player);
                timer = 0.125f;
            }
            Vector3 rot = player.position - transform.position;
            float rotZ = Mathf.Atan2(rot.y,rot.x) * Mathf.Rad2Deg;
            pivotPoint.transform.rotation = Quaternion.Euler(0,0,rotZ);

            float distanceToPlayer = (player.position - transform.position).magnitude;
            if (distanceToPlayer >= 15f){
                Vector3 dir = player.position - transform.position;
                RB.velocity = new Vector2(dir.x,dir.y).normalized*4f;
            }
            else{
                RB.velocity = new Vector2(0,0);
            }
        }

        

        
    }
    private void shootMiniCannons()
    {
        for(int i = 0; i<8; i++){
            Transform spawner = minicannons[i].transform.GetChild(0);
            GameObject newBullet = Instantiate(bullet,spawner.position,Quaternion.identity);
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
            newBullet.GetComponent<Enemybullet>().setDamage(15f);
            Vector3 dir = spawner.position - gameObject.transform.position;
            bulletRb.velocity = new Vector2(dir.x,dir.y).normalized * 12.5f;
        }
        
    }
    private void shootCircle() {
        for (int i = 0; i < 32; i++){
            GameObject newBullet = Instantiate(bullet,transform.position,Quaternion.identity);
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
            newBullet.GetComponent<Enemybullet>().setDamage(15f);

            bulletRb.velocity = new Vector2(math.cos((i*15f) * math.PI/180f),math.sin((i*15f) * math.PI/180f)).normalized *7.5f;
            FindAnyObjectByType<AudioManager>().Play("shoot",GunContainer.sniper.pitch,GunContainer.sniper.volume);
        }
    }
    private void shootMainCannon(Transform target){
        Transform spawner = mainCannon.transform.GetChild(0);
        GameObject newBullet = Instantiate(bullet,spawner.position,Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        newBullet.GetComponent<Enemybullet>().setDamage(15f);

        Vector2 bulletDirection = target.position - transform.position;
        bulletDirection += Vector2.Perpendicular(bulletDirection) * UnityEngine.Random.Range(-0.45f,0.45f);
        bulletRb.velocity = new Vector2(bulletDirection.x,bulletDirection.y).normalized * 20f;
        FindAnyObjectByType<AudioManager>().Play("shoot",GunContainer.AR.pitch,GunContainer.AR.volume);
    }
    private void shootShotgun(Transform target){
        for(int i = -2; i<3;i++){
            Transform spawner = mainCannon.transform.GetChild(0);
            GameObject newBullet = Instantiate(bullet,spawner.position,Quaternion.identity);
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
            newBullet.GetComponent<Enemybullet>().setDamage(15f);
            Vector2 bulletDirection = target.position - transform.position;
            bulletDirection += Vector2.Perpendicular(bulletDirection) * i*0.35f;
            bulletRb.velocity = new Vector2(bulletDirection.x,bulletDirection.y).normalized * 15f;
        }
        FindAnyObjectByType<AudioManager>().Play("shoot",GunContainer.shotgun.pitch,GunContainer.shotgun.volume);
    }


    private void DebugArrayContents()
    {
        // list the result
        foreach (string s in atkArray)
        {
            Debug.Log(s);
        }
    }

    private void FillArray()
    {
        // fill with some data
        for (int i = 0; i < atkArray.Length; i++)
        {
            atkArray[i] = "atk" + i.ToString();
        }
        Array.Sort(atkArray, RandomSort);
    }

    int RandomSort(string a, string b)
    {
        return UnityEngine.Random.Range(-1, 2);

    }
    public void spnDeath(){

        GameObject effect = Instantiate(deathEffect,transform.position,Quaternion.identity);
        Gradient grad = new Gradient();

        grad.SetKeys( new GradientColorKey[] 
        { new GradientColorKey(new Color(1,0,0), 0.0f), 
        new GradientColorKey(new Color(1f, 1f, 1f, 0), 1.0f) }, 
        new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
        new GradientAlphaKey(0.0f, 1.5f) } );

        var col = effect.GetComponent<ParticleSystem>().colorOverLifetime;
        col.enabled = true;
        col.color = grad;
        effect.transform.localScale = gameObject.transform.localScale;

        Destroy(gameObject);
    }
}