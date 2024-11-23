using UnityEngine;

public class GunContainer : MonoBehaviour
{
    public static readonly Gun pistol = new Gun(0.15f,0.1f,1f,1f,15f,1.25f);
    public static readonly Gun shotgun = new Gun(0.275f,0.25f,5f,0.75f,10f,2.5f,-1.4f,0.2f);
    public static readonly Gun sniper = new Gun(0.45f,0f,1f,2f,50f,5.5f,-1f,0.25f);

    public static readonly Gun AR = new Gun(0.075f,0.1f,1f,1.35f,15f,1f,0.2f);
    public static readonly Gun badPistol = new Gun(0.75f,0f,1f,1f,5f,1f);

    public static readonly Gun badShotgun = new Gun(0.45f,0.25f,5f,0.75f,7f,1f,-1.4f,0.2f);

    public static readonly Gun badSniper = new Gun(0.9f,0f,1f,1.7f,15f,5f,-1f,0.25f);

    

} 