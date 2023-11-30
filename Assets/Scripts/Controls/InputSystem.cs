public abstract class InputSystem
{
    public abstract float GetInput(InputSignal signal);
}

public enum InputSignal : int
{
    ForwardBack = 0,
    RightLeft = 1
}