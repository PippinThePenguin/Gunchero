using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ET = UnityEngine.InputSystem.EnhancedTouch;

//Taking player input via joystick, returning stick position as vector normalized by joystick size,
//creating "floating joystick" effect
public class PlayerInput : MonoBehaviour {
  public event Action<Vector2> OnValueChange;

  [SerializeField] private float _joystickSize = 150f;
  [SerializeField] private Joystick _joystick;

  private Finger _joystickFinger;
  private bool _joysstickEnabled = false;

  private void OnEnable() {
    LevelFlow.OnLevelStart += EnableJoystick;
    LevelFlow.OnLevelResume += EnableJoystick;
    LevelFlow.OnLevelPause += DisableJoystick;
    LevelFlow.OnLevelEnd += DisableJoystick;
    LevelFlow.OnLevelEnd += DisableJoystick;
  }

  private void OnDisable() {
    LevelFlow.OnLevelStart -= EnableJoystick;
    LevelFlow.OnLevelResume -= EnableJoystick;
    LevelFlow.OnLevelPause -= DisableJoystick;
    LevelFlow.OnLevelEnd -= DisableJoystick;
    LevelFlow.OnLevelEnd -= DisableJoystick;
  }

  private void EnableJoystick() {
    if (_joysstickEnabled) {
      return;
    }
    EnhancedTouchSupport.Enable();
    ET.Touch.onFingerDown += HandleFingerDown;
    ET.Touch.onFingerUp += HandleFingerUp;
    ET.Touch.onFingerMove += HandleFingerMove;
    _joysstickEnabled = true;
  }

  private void DisableJoystick() {
    if (!_joysstickEnabled) {
      return;
    }
    ET.Touch.onFingerDown -= HandleFingerDown;
    ET.Touch.onFingerUp -= HandleFingerUp;
    ET.Touch.onFingerMove -= HandleFingerMove;
    EnhancedTouchSupport.Disable();
    _joysstickEnabled = false;
  }
 
  private void HandleFingerDown(Finger TouchedFinger) {
    if (_joystickFinger != null) {
      return;
    }
    _joystickFinger = TouchedFinger;
    _joystick.gameObject.SetActive(true);
    _joystick.SetRadius(_joystickSize);
    _joystick.SetPosition(TouchedFinger.screenPosition);
  }

  private void HandleFingerUp(Finger LostFinger) {
    if (LostFinger != _joystickFinger) {
      return;
    }
    _joystickFinger = null;
    _joystick.gameObject.SetActive(false);
    OnValueChange?.Invoke(Vector2.zero);
  }
  
  private void HandleFingerMove(Finger MovedFinger) {
    if (MovedFinger != _joystickFinger) {
      return;
    }
    _joystick.SetKnobPosition(MovedFinger.currentTouch.screenPosition);
    OnValueChange?.Invoke(_joystick.GetDirections());
  }
}


