using UnityEngine;

public class GunContainer : MonoBehaviour
{
    public static readonly Gun pistol = new Gun(0.1f,0.1f,1f,1f,15f);
    public static readonly Gun shotgun = new Gun(0.2f,0.25f,5f,1f,10f);
    public static readonly Gun sniper = new Gun(0.3f,0f,1f,1f,30f);

    public static readonly Gun gun = new Gun(0.05f,0.2f,10f,1f,30f);
    public static readonly Gun badPistol = new Gun(0.75f,0f,1f,1f,5f);
    

} 