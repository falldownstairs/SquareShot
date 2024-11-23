using UnityEngine;
using UnityEngine.UI;

public class BossHealthScript : MonoBehaviour
{
    public Image health;
    public GameObject healthBar;

    public GameObject healthCanvas;
    
    float maxHealth = 5500f;
    float totalHealth = 5500f;

    private bool bossDead = false;

    public static BossHealthScript Instance {get; private set;}


    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
    }
    void Update()
    {
        if (totalHealth <= 0 && bossDead == false){
            bossDead = true;
            string t = Timer.Instance.EndTimer();
            setCanvasActive(false);
            GameObject.FindGameObjectWithTag("BossRoom").GetComponent<BossRoom>().clearRoom();
            StartCoroutine(GameScript.Instance.Win());
            GameObject boss = GameObject.FindGameObjectWithTag("boss");
            boss.GetComponent<BossScript>().spnDeath();

        }
    }
    public void LoseHealth(float loseAmmount)
    {
        totalHealth -= loseAmmount;
        health.fillAmount = totalHealth/maxHealth;
    }
    public void GainHealth(float gainAmmount)
    {
        totalHealth += gainAmmount;
        health.fillAmount = totalHealth/maxHealth;
    }
    public void setCanvasActive(bool n){
        healthCanvas.SetActive(n);
    }
}
