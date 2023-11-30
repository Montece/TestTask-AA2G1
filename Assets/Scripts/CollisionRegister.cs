using UnityEngine;

public class CollisionRegister : MonoBehaviour
{
    [SerializeField] private CollisionType Type;
    [SerializeField] private HealthSystem HealthSystem;
    [SerializeField] private bool UseReload;
    [SerializeField] private float ReloadTime;

    private float currentReload;
    private bool canDoCollision = true;

    private void Update()
    {
        if (currentReload > 0f) currentReload -= Time.deltaTime;
        if (currentReload <= 0f) canDoCollision = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canDoCollision) return;

        if (collision != null && collision.transform.TryGetComponent(out CollisionRegister register))
        {
            if (register.GetHealthSystem() == null) return;
            if (register.GetHealthSystem() == HealthSystem) return;

            //print($"{Type} -> {register.Type}");

            switch (Type)
            {
                case CollisionType.Bike:
                    if (register.Type == CollisionType.Bike) register.GetHealthSystem().Damage(HealthSystem.BIKE_BIKE_DAMAGE);
                    break;
                case CollisionType.Spear:
                    if (register.Type == CollisionType.Bike) register.GetHealthSystem().Damage(HealthSystem.SPEAR_BIKE_DAMAGE);
                    if (register.Type == CollisionType.Shield) register.GetHealthSystem().Damage(HealthSystem.SPEAR_SHIELD_DAMAGE);
                    break;
                default:
                    break;
            }

            canDoCollision = false;
            currentReload = ReloadTime;
        }
    }

    public HealthSystem GetHealthSystem()
    {
        return HealthSystem;
    }
}

public enum CollisionType : int
{
    Bike = 0,
    Spear = 1,
    Shield = 2
}