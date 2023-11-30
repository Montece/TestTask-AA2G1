using UnityEngine;

[RequireComponent(typeof(HealthSystem), typeof(MovementSystem))]
public class EntityLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] Models;
    [HideInInspector] public HealthSystem HealthSystem;
    [HideInInspector] public MovementSystem MovementSystem;

    protected void Awake()
    {
        HealthSystem = GetComponent<HealthSystem>();
        MovementSystem = GetComponent<MovementSystem>();
        HealthSystem.OnHealthChange += HealthSystem_OnHealthChange;
    }

    private void HealthSystem_OnHealthChange(int newHealth)
    {
        if (newHealth <= 0f)
        {
            for (int i = 0; i < Models.Length; i++) Models[i].SetActive(false);
            HealthSystem.enabled = false;
            MovementSystem.enabled = false;
            MovementSystem.BlockMovement = true;
            GetComponent<CharacterController>().enabled = false;
        }
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
}
