using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private const float GRAVITY_NORMAL = 0.7f;
    public static Lander Instance {  get; private set; }

    private Rigidbody2D landerRigidbody2D;
    public event EventHandler OnLeftForce;
    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;

    }
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs : EventArgs
    {
        public LandingType landingType;
        public float score;
        public float relativeVelocity;
        public float angleLanding;
        public int multiplier;
    }
    public enum State
    {
        WaitingToStart,
        Normal,
        GameOver,
    }
    private State state;
    public enum LandingType
    {
        Success, LandingOnTerrain, LandingUnstable, LandingTooHard,
    }

    private float maxFuelAmount = 100f;
    private float fuelAmount;
    private float pushUpFuelRequire = 10f;
    private float sideEngineFuelRequire = 3f;

    private void Awake()
    {
        Instance = this;

        fuelAmount = maxFuelAmount;
        landerRigidbody2D = GetComponent<Rigidbody2D>();
        landerRigidbody2D.gravityScale = 0f;
    }
    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if(Keyboard.current.wKey.isPressed || 
                    Keyboard.current.aKey.isPressed ||
                    Keyboard.current.dKey.isPressed)
                {
                    landerRigidbody2D.gravityScale = GRAVITY_NORMAL;
                    SetState(State.Normal);
                }
                break;
            case State.Normal:
                if (fuelAmount <= 0)
                {
                    Debug.Log("out of fuel!");
                    return;
                }
                if (Keyboard.current.wKey.isPressed)
                {
                    float pushForce = 700f;
                    landerRigidbody2D.AddForce(pushForce * transform.up * Time.deltaTime);
                    FuelConsume(pushUpFuelRequire);
                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }
                if (Keyboard.current.aKey.isPressed)
                {
                    float rotateForce = +75f;
                    landerRigidbody2D.AddTorque(rotateForce * Time.deltaTime);
                    FuelConsume(sideEngineFuelRequire);
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                if (Keyboard.current.dKey.isPressed)
                {
                    float rotateForce = -75f;
                    landerRigidbody2D.AddTorque(rotateForce * Time.deltaTime);
                    FuelConsume(sideEngineFuelRequire);
                    OnLeftForce?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    private void SetState(State state)
    {
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = state
        });

    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        float softLandingVelocityMagnitude = 4f;
        float landingVelocityRelative = collision2D.relativeVelocity.magnitude;
        float landingAngle = Vector2.Dot(Vector2.up, transform.up);
        float landingAngleAllow = 0.9f;

        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingType = LandingType.LandingOnTerrain,
                angleLanding = 0,
                relativeVelocity = 0,
                multiplier = 0,
                score = 0,
            });
            SetState(State.GameOver);
            return;
        }
        if (landingVelocityRelative > softLandingVelocityMagnitude)
        {
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingType = LandingType.LandingTooHard,
                angleLanding = landingAngle,
                relativeVelocity = landingVelocityRelative,
                multiplier = 0,
                score = 0,
            });
            SetState(State.GameOver);
            return;
        } 
        if (landingAngle < landingAngleAllow)
        {
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingType = LandingType.LandingUnstable,
                angleLanding = landingAngle,
                relativeVelocity = landingVelocityRelative,
                multiplier = 0,
                score = 0,
            }); 
            SetState(State.GameOver);
            return;
        }

        float maxLandingAngleScore = 100;
        int scoreDotAngleMultiplier = 10;
        float landingAngleScore = maxLandingAngleScore - Mathf.Abs(1 - landingAngle) * scoreDotAngleMultiplier * maxLandingAngleScore;

        float maxLandingVelocityScore = 100;
        float landingVelocityScore = (softLandingVelocityMagnitude - landingVelocityRelative) * maxLandingVelocityScore;

        int multiplierScore = landingPad.GetMultiplierScore();
        float totalScore = (landingAngleScore + landingVelocityScore) * multiplierScore;

        
        OnLanded?.Invoke(this, new OnLandedEventArgs
        {
            landingType = LandingType.Success,
            angleLanding = landingAngle,
            relativeVelocity = landingVelocityRelative,
            multiplier = multiplierScore,
            score = totalScore,
        });
        SetState(State.GameOver);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.TryGetComponent(out Fuel fuel))
        {
            float fuelRefill = 100f;
            FuelRefill(fuelRefill);
            fuel.SelfDestroy();
        }

        if(collider2D.gameObject.TryGetComponent(out Coin coin))
        {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coin.SelfDestroy();
        }
    }

    private void FuelConsume(float fuelConsume)
    {
        fuelAmount -= fuelConsume * Time.deltaTime;
    }

    private void FuelRefill(float fuelRefill)
    {
        fuelAmount += fuelRefill;
        if (fuelAmount > maxFuelAmount)
        {
            fuelAmount = maxFuelAmount;
        }
    }

    public float GetFuelAmount()
    {
        return fuelAmount/maxFuelAmount;
    }

   public float GetSpeedX()
    {
        return landerRigidbody2D.linearVelocityX;
    }
    public float GetSpeedY()
    {
        return landerRigidbody2D.linearVelocityY;
    }

}
