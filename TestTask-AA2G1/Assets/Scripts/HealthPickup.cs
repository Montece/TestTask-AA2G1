using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int HealPower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Heal(HealPower);
            Destroy(gameObject);
        }
    }
}
