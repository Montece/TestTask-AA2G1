using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health System")]
    [SerializeField] private int MaximumHealth = 10;
    [SerializeField] private int CurrentHealth = 0;
    [SerializeField] private bool StartHealthIsMaximum = true;
    [SerializeField] public bool GodMode = false;

    public const int BIKE_BIKE_DAMAGE = 1;
    public const int SPEAR_BIKE_DAMAGE = 4;
    public const int SPEAR_SHIELD_DAMAGE = 1;

    public int Health => CurrentHealth;
    public int MaxHealth => MaximumHealth;
    public event Action<int> OnHealthChange;

    private void Awake()
    {
        if (StartHealthIsMaximum) CurrentHealth = MaximumHealth;
    }

    public bool Damage(int damage)
    {
        if (GodMode) return false;

        CurrentHealth -= damage;
        OnHealthChange(CurrentHealth);

        return true;
    }

    public bool Heal(int power)
    {
        CurrentHealth += power;
        if (CurrentHealth > MaximumHealth) CurrentHealth = MaximumHealth;
        OnHealthChange(CurrentHealth);

        return true;
    }
}
