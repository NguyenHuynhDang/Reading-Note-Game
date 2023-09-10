using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    public event EventHandler<OnPitchToggleEventArgs> OnPlayPitch;
    public event EventHandler<OnPitchToggleEventArgs> OnReleasePitch;
    
    private PlayerInputAction _playerInputAction;

    protected GameInput() {} // protected constructor

    private void Awake()
    {
        Instance = this;
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();
    }

    private void Start()
    {
        _playerInputAction.Player.PlayAPitch.performed += APitch_performed;
        _playerInputAction.Player.PlayBPitch.performed += BPitch_performed;
        _playerInputAction.Player.PlayCPitch.performed += CPitch_performed;
        _playerInputAction.Player.PlayDPitch.performed += DPitch_performed;
        _playerInputAction.Player.PlayEPitch.performed += EPitch_performed;
        _playerInputAction.Player.PlayFPitch.performed += FPitch_performed;
        _playerInputAction.Player.PlayGPitch.performed += GPitch_performed;

        _playerInputAction.Player.PlayAPitch.canceled += APitch_canceled;
        _playerInputAction.Player.PlayBPitch.canceled += BPitch_canceled;
        _playerInputAction.Player.PlayCPitch.canceled += CPitch_canceled;
        _playerInputAction.Player.PlayDPitch.canceled += DPitch_canceled;
        _playerInputAction.Player.PlayEPitch.canceled += EPitch_canceled;
        _playerInputAction.Player.PlayFPitch.canceled += FPitch_canceled;
        _playerInputAction.Player.PlayGPitch.canceled += GPitch_canceled;
    }

    private void APitch_performed(InputAction.CallbackContext obj)
    {
        OnPlayPitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.A });
    }
    private void BPitch_performed(InputAction.CallbackContext obj)
    {
        OnPlayPitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.B} );
    }
    private void CPitch_performed(InputAction.CallbackContext obj)
    {
        OnPlayPitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.C} );
    }
    private void DPitch_performed(InputAction.CallbackContext obj)
    {
        OnPlayPitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.D} );
    }
    private void EPitch_performed(InputAction.CallbackContext obj)
    {
        OnPlayPitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.E} );
    }
    private void FPitch_performed(InputAction.CallbackContext obj)
    {
        OnPlayPitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.F} );
    }
    private void GPitch_performed(InputAction.CallbackContext obj)
    {
        OnPlayPitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.G} );
    }
    
    private void APitch_canceled(InputAction.CallbackContext obj)
    {
        OnReleasePitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.A });
    }
    private void BPitch_canceled(InputAction.CallbackContext obj)
    {
        OnReleasePitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.B });
    }
    private void CPitch_canceled(InputAction.CallbackContext obj)
    {
        OnReleasePitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.C });
    }
    private void DPitch_canceled(InputAction.CallbackContext obj)
    {
        OnReleasePitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.D });
    }
    private void EPitch_canceled(InputAction.CallbackContext obj)
    {
        OnReleasePitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.E });
    }
    private void FPitch_canceled(InputAction.CallbackContext obj)
    {
        OnReleasePitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.F });
    }
    private void GPitch_canceled(InputAction.CallbackContext obj)
    {
        OnReleasePitch?.Invoke(this, new OnPitchToggleEventArgs() { PitchToggle = Pitch.G });
    }
    
}
