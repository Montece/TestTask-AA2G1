using TMPro;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private string TextFormat;
    [SerializeField] private HealthSystem healthSystem;

    private TMP_Text Text;

    private void Awake()
    {
        Text = GetComponent<TMP_Text>();
        if (healthSystem != null) healthSystem.OnHealthChange += HealthSystem_OnHealthChange;
    }

    private void Start()
    {
        SetText(healthSystem.Health);
    }

    private void HealthSystem_OnHealthChange(int newHealth)
    {
        SetText(newHealth);
    }

    private void SetText(int health)
    {
        Text.text = string.Format(TextFormat, health);
    }
}
