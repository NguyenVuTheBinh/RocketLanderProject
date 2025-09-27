using UnityEngine;

public class GameInput : MonoBehaviour
{
    private InputActions inputActions;
    
    public static GameInput Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();
    }
    private void OnDestroy()
    {
        inputActions.Disable();
    }

    public bool IsUpActionPressed()
    {
        return inputActions.Player.LanderUp.IsPressed();
    }
    public bool IsLeftActionPressed()
    {
        return inputActions.Player.LanderLeft.IsPressed();
    }
    public bool IsRightActionPressed()
    {
        return inputActions.Player.LanderRight.IsPressed();
    }
    public Vector2 GetMovementAction()
    {
        return inputActions.Player.Movement.ReadValue<Vector2>();
    }
}
