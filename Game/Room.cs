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
    public bool roomCleared = false;
    public int waves = 0;
    public int enemiesLeft = 0;

    private bool triggered = false;


    void Update()
    {
        if (triggered)
        {
            if (waves > 0 && enemiesLeft == 0)
            {
                spawnWave();
                waves -= 1;
            }
            if (waves == 0 && enemiesLeft == 0)
            {
                setCleared(true);
                setDoors(false);
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
    private void setDoors(bool b)
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
            waves += Random.Range(2,4);
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
