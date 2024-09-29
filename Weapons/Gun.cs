public class Gun
{
    public float fireRate;
    public float spread;  
    public float bullets;
    public float speedMultiplier;
    public float damage;

    public Gun(float _fireRate, float _spread, float _bullets, float _speedMultiplier, float _damage)
    {
        fireRate = _fireRate;
        spread = _spread;
        bullets = _bullets;
        speedMultiplier = _speedMultiplier;
        damage = _damage;
    }
    
}
