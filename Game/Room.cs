using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int RoomIndex {get; set;}

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] GameObject topCorridor;
    [SerializeField] GameObject bottomCorridor;
    [SerializeField] GameObject leftCorridor;
    [SerializeField] GameObject rightCorridor;

    [SerializeField] GameObject staminaBuff;
    [SerializeField] GameObject fireRateBuff;
    [SerializeField] GameObject healthExpand;
    [SerializeField] GameObject bulletBuff;
    [SerializeField] GameObject AR;

    [SerializeField] GameObject minimapDisplay;
    public bool roomCleared = false;
    public int waves = 0;
    public int enemiesLeft = 0;

    public bool triggered = false;

    public bool colorChange = false;

    Dictionary<int,GameObject> dict = new Dictionary<int, GameObject>();

    void Awake()
    {
        dict.Add(1,staminaBuff);
        dict.Add(2,fireRateBuff);
        dict.Add(4,AR);
        dict.Add(3,healthExpand);
    }
    void Update()
    {
        if(roomCleared && colorChange == false){
            minimapDisplay.GetComponent<SpriteRenderer>().color = new Color(0.37f,0.71f,0.25f,0.25f);
            colorChange = true;
        }
        if (triggered && roomCleared == false)
        {
            if (waves > 0 && enemiesLeft == 0)
            {
                spawnWave();
                waves -= 1;
            }
            if (waves == 0 && enemiesLeft == 0)
            {
                int rand;
                if (GunScript.Instance.getGun() == GunContainer.AR){
                    rand = Random.Range(1,3);
                }
                else{
                    rand = Random.Range(1,4);
                }
                setCleared(true);
                setDoors(false);
                Instantiate(dict[rand],gameObject.transform.position,Quaternion.identity);
                
            }
            
        }
        
    }
    public void OpenDoor(Vector2Int direction)
    {
        if(direction == Vector2Int.up)
        {
            topDoor.SetActive(false);
            topCorridor.SetActive(true);
        }
        if(direction == Vector2Int.down)
        {
            bottomDoor.SetActive(false);
            bottomCorridor.SetActive(true);
        }
        if(direction == Vector2Int.left)
        {
            leftDoor.SetActive(false);
            leftCorridor.SetActive(true);
        }
        if(direction == Vector2Int.right)
        {
            rightDoor.SetActive(false);
            rightCorridor.SetActive(true);
        }
    }

    public void setDoors(bool b)
    {
        if (topCorridor.activeSelf == true)
        {
            topDoor.SetActive(b);
        }
        if (bottomCorridor.activeSelf == true)
        {
            bottomDoor.SetActive(b);
        }
        if (leftCorridor.activeSelf == true)
        {
            leftDoor.SetActive(b);
        }
        if (rightCorridor.activeSelf == true)
        {
            rightDoor.SetActive(b);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Player") & roomCleared == false)
       {
            Debug.Log("triggered");
            triggered = true;
            setDoors(true);
            waves += Random.Range(1,3);
       }
    }
    private void spawnWave()
    {
        int nEmemies = Random.Range(2,5);
        for(int i = 0; i<nEmemies; i++)
        {
            Vector2 p = new Vector2(transform.position.x+Random.Range(-17,17),
                transform.position.y+Random.Range(-17,17));
            GameObject e = Instantiate(enemy,p,Quaternion.identity);
            e.GetComponent<EnemyScript>().setRoom(gameObject.GetComponent<Room>());
        }
        enemiesLeft = nEmemies;
    }
    public void enemyDeath(){enemiesLeft -= 1;}
    public void setCleared(bool b){roomCleared = b;}

}