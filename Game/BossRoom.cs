using Cinemachine;
using UnityEngine;
public class BossRoom : Room
{
    GameObject vCam;
    [SerializeField] private GameObject boss;

    [SerializeField] private GameObject minimapD;

    private float t;
    float c = 0f;
    private bool spawnBoss = false;

    void Awake()
    {
        vCam = GameObject.FindGameObjectWithTag("virtualCam");

    }
    void Update()
    {
        t -= Time.deltaTime;
        if(spawnBoss && t <= 0){
            BossHealthScript.Instance.setCanvasActive(true);
            Instantiate(boss,gameObject.transform.position,Quaternion.identity);
            spawnBoss = false;
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
        {
        if(collision.gameObject.CompareTag("Player") & roomCleared == false)
        {
                Debug.Log("triggered");
                triggered = true;
                setDoors(true);
                spawnBoss = true;
                vCam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 20f;
                FindAnyObjectByType<AudioManager>().Stop("battleTheme");
                FindAnyObjectByType<AudioManager>().Play("bossTheme");
                t = 3f;
    
                
        }
    }
    public void clearRoom(){
        minimapD.GetComponent<SpriteRenderer>().color = new Color(0.37f,0.71f,0.25f,0.25f);

    }

}
