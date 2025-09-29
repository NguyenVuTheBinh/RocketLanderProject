using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    private InputActions inputActions;

    public event EventHandler OnPauseButtonPressed;
    
    public static GameInput Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.PauseGame.performed += PauseGame_performed;
    }

    private void PauseGame_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseButtonPressed?.Invoke(this, EventArgs.Empty);
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
