using UnityEngine;

public class PlayerLogic : EntityLogic
{
    public InputSystem InputSystem { get; private set; }

    private new void Awake()
    {
        base.Awake();

#if UNITY_STANDALONE
        InputSystem = new PCInput();
        GameManager.Instance.ForwardBackControls.SetActive(false);
        GameManager.Instance.RightLeftControls.SetActive(false);
        //InputSystem = new MobileInput();
#endif

#if UNITY_ANDROID
        InputSystem = new MobileInput();
        GameManager.Instance.ForwardBackControls.SetActive(true);
        GameManager.Instance.RightLeftControls.SetActive(true);
#endif
    }

    private void Update()
    {
        float forwardBack = InputSystem.GetInput(InputSignal.ForwardBack);
        float rightLeft = InputSystem.GetInput(InputSignal.RightLeft);

        MovementSystem.DoMovement(moving: forwardBack, rotation: rightLeft);
    }
}