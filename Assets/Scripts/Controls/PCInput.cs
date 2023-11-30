using UnityEngine;

public class PCInput : InputSystem
{
    public override float GetInput(InputSignal signal)
    {
        return signal switch
        {
            InputSignal.ForwardBack => Input.GetAxis("Vertical"),
            InputSignal.RightLeft => Input.GetAxis("Horizontal"),
            _ => 0f,
        };
    }
}