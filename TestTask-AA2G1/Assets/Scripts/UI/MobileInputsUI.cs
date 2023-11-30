using UnityEngine;

public class MobileInputsUI : MonoBehaviour
{
    private PlayerLogic playerLogic;

    private void Awake()
    {
        playerLogic = FindObjectOfType<PlayerLogic>();
    }

    public void StartMoveForward()
    {
        if (playerLogic == null) return;

        (playerLogic.InputSystem as MobileInput).Forward = 1f;
    }

    public void EndMoveForward()
    {
        if (playerLogic == null) return;

        (playerLogic.InputSystem as MobileInput).Forward = 0f;
    }

    public void StartMoveBack()
    {
        if (playerLogic == null) return;

        (playerLogic.InputSystem as MobileInput).Back = 1f;
    }

    public void EndMoveBack()
    {
        if (playerLogic == null) return;

        (playerLogic.InputSystem as MobileInput).Back = 0f;
    }

    public void StartMoveRight()
    {
        if (playerLogic == null) return;

        (playerLogic.InputSystem as MobileInput).Right = 1f;
    }

    public void EndMoveRight()
    {
        if (playerLogic == null) return;

        (playerLogic.InputSystem as MobileInput).Right = 0f;
    }

    public void StartMoveLeft()
    {
        if (playerLogic == null) return;

        (playerLogic.InputSystem as MobileInput).Left = 1f;
    }

    public void EndMoveLeft()
    {
        if (playerLogic == null) return;

        (playerLogic.InputSystem as MobileInput).Left = 0f;
    }
}
