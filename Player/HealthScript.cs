using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Image healthBar;
    float totalHealth = 100f;
    void Update()
    {
        if (totalHealth == 0)
        {
            GameScript.Instance.GameOver();
        }
    }
    public void LoseHealth(float loseAmmount)
    {
        totalHealth -= loseAmmount;
        healthBar.fillAmount = totalHealth/100f;

    }
    public void GainHealth(float gainAmmount)
    {
        totalHealth += gainAmmount;
        Mathf.Clamp(totalHealth,0,100);

        healthBar.fillAmount = totalHealth/100f;
    }
}
