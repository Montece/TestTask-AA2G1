using UnityEngine;

public class AILogic : EntityLogic
{
    //ИИ на основе простой FSM
    [SerializeField] private EntityLogic AttackTarget;
    [SerializeField] private bool IsAttacking = true;

    private AIState currentState;
    private GameObject healthPickup = null;

    private void Start()
    {
        ChangeState(AIState.Idle);
    }

    private void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:
                //Условие перехода
                if (IsAttacking)
                {
                    ChangeState(AIState.Attack);
                    break;
                }
                //Действие
                break;
            case AIState.Attack:
                //Условие перехода
                if (!IsAttacking || AttackTarget == null)
                {
                    ChangeState(AIState.Idle);
                    break;
                }
                if (HealthSystem.Health <= HealthSystem.MaxHealth / 2f)
                {
                    HealthPickup hp = FindObjectOfType<HealthPickup>();
                    if (hp != null)
                    {
                        healthPickup = hp.gameObject;
                        ChangeState(AIState.Healing);
                        break;
                    }
                }
                //Действие
                Vector3 targetPosition = AttackTarget.GetWorldPosition();
                Vector3 moveDirection = targetPosition - GetWorldPosition();
                float angle = Vector3.SignedAngle(transform.forward, moveDirection, Vector3.up);
                float moveFloat = angle > 0f ? 1f : -1f;
                MovementSystem.DoMovement(1f, moveFloat);
                break;
            case AIState.Healing:
                //Условие перехода
                if (healthPickup == null)
                {
                    ChangeState(AIState.Idle);
                    break;
                }
                //Действие
                Vector3 healingPosition = healthPickup.transform.position;
                Vector3 moveToHealingDirection = healingPosition - GetWorldPosition();
                float angleToHeal = Vector3.SignedAngle(transform.forward, moveToHealingDirection, Vector3.up);
                float moveToHealFloat = angleToHeal > 0f ? 1f : -1f;
                MovementSystem.DoMovement(1f, moveToHealFloat);
                break;
            default:
                break;
        }
    }

    private void ChangeState(AIState newState)
    {
        currentState = newState;
    }
}

public enum AIState : int
{
    Idle = 0,
    Attack = 1,
    Healing = 2
}