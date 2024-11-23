using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Image health;
    public GameObject healthBar;
    float totalHealth = 100f;
    float maxHealth = 100f;

    private float iframes;

    public static HealthScript Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
    }
    void Update()
    {
        if (totalHealth <= 0)
        {
            GameScript.Instance.GameOver();
        }
        if (totalHealth > maxHealth){totalHealth = maxHealth;}
        iframes -= Time.deltaTime;
    }
    public void LoseHealth(float loseAmmount)
    {
        if (iframes <= 0){
            totalHealth -= loseAmmount;
            health.fillAmount = totalHealth/maxHealth;
        }
    }
    public void GainHealth(float gainAmmount)
    {
        totalHealth += gainAmmount;
        health.fillAmount = totalHealth/maxHealth;
    }

    public void expandHealth(int expandAmmount)
    {
        healthBar.transform.localScale += new Vector3(expandAmmount,0,0);
        healthBar.transform.localPosition += new Vector3(expandAmmount*49f,0,0);
        maxHealth += expandAmmount * 20f;
        totalHealth += expandAmmount * 20f;
        
    }
    public void getIframes(float i)
    {
        iframes = i;
    }
    public bool iframesActive(){return (iframes > 0);}
    public bool isMaxHealth(){return totalHealth == maxHealth;}

}
