public class MobileInput : InputSystem
{
    public float Forward { get; set; } = 0f;
    public float Back { get; set; } = 0f;
    public float Left { get; set; } = 0f;
    public float Right { get; set; } = 0f;

    public override float GetInput(InputSignal signal)
    {
        return signal switch
        {
            InputSignal.ForwardBack => Forward - Back,
            InputSignal.RightLeft => Right - Left,
            _ => 0f,
        };
    }
}