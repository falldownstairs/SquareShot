public class Gun
{
    public float fireRate;
    public float spread;  
    public float bullets;
    public float bulletSpeed;
    public float damage;

    public float staminaDrain;

    public float pitch;

    public float volume;

    public Gun(float _fireRate, float _spread, float _bullets, float _bulletSpeed, float _damage, float _staminaDrain, float _pitch = 0, float _volume = 0)
    {
        fireRate = _fireRate;
        spread = _spread;
        bullets = _bullets;
        bulletSpeed = _bulletSpeed;
        damage = _damage;
        staminaDrain = _staminaDrain;
        pitch = _pitch;
        volume = _volume;



    }
}
