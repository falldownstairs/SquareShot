using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    public Image Stamina;
    public GameObject StaminaBar;
    float totalStamina = 80f;
    float maxStamina = 80f;
    private bool regen = false;
    private float rechargeTimeout = 0;

    private bool canUseStamina = true;
    public static StaminaScript Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
    }
    void Update()
    {
        if (regen == false && rechargeTimeout <= 0)
        {
            StartCoroutine(Regen());
        }
        if (totalStamina < 0){totalStamina = 0;}

        if (totalStamina == 0)
        {
            totalStamina += 1f;
            StartCoroutine(Burnout());
            rechargeTimeout = 0.65f;
        }

        if (totalStamina > maxStamina)
        {
            totalStamina = maxStamina;
        }
        if (totalStamina <= 1.1f){Stamina.fillAmount = 0f;}
        else {Stamina.fillAmount = totalStamina/maxStamina;}
        rechargeTimeout -= Time.deltaTime;
    }
    public void LoseStamina(float loseAmmount)
    {
        totalStamina -= loseAmmount;
    }
    
    public void setRechargeTimer()
    {
        rechargeTimeout = 0.35f;
    }

    public void expandStamina(int expandAmmount)
    {
        StaminaBar.transform.localScale += new Vector3(expandAmmount,0,0);
        StaminaBar.transform.localPosition += new Vector3(expandAmmount*49f,0,0);
        maxStamina += expandAmmount * 20f;
        totalStamina += expandAmmount * 20f;
        
    }
    private IEnumerator Regen()
    {
        regen = true;
        totalStamina += 2.5f;
        yield return new WaitForSeconds(0.1f);
        regen = false;
    }
    private IEnumerator Burnout()
    {
        canUseStamina = false;
        Stamina.color = new Color(48,150,0);
        yield return new WaitForSeconds((totalStamina/2.5f)/0.1f-0.05f);
        Stamina.color = new Color(0,255,0);

        canUseStamina = true;

    }
    public float getStamina()
    {
        return totalStamina;
    }
    public bool CanUseStamina()
    {
        return canUseStamina;
    }

}
