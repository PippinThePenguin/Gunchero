using System;
using UnityEngine;

//Joystick
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class Joystick : MonoBehaviour {
  public float Radius => _radius;
  public Vector2 JoystickPosition => _position;
  
  [SerializeField] private RectTransform _knob;

  private float _radius;
  private Vector2 _position;
  private RectTransform _rectTransform;

  private void Awake() {
    _rectTransform = GetComponent<RectTransform>();
    SetRadius(_radius);
    _knob.anchoredPosition = Vector2.zero;
  }

  private void OnDisable() {
    _knob.anchoredPosition = Vector2.zero;
  }

  public void SetRadius(float radius) {
    if (radius < 0) {
      throw new ArgumentOutOfRangeException(nameof(radius));
    }
    _radius = radius;
    _rectTransform.sizeDelta = new Vector2(radius * 2, radius * 2);
    _knob.sizeDelta = new Vector2(radius * 2, radius * 2);
  }

  public void SetPosition(Vector2 position) {
    if (position.x < Radius) {
      position.x = Radius;
    } else if (position.x > Screen.width - Radius) {
      position.x = Screen.width - Radius;
    }
    if (position.y < Radius) {
      position.y = Radius;
    } else if (position.y > Screen.height - Radius) {
      position.y = Screen.height - Radius;
    }
    _position = position;
    _rectTransform.anchoredPosition = _position;
  }

  public void SetKnobPosition(Vector2 knobPosition) {
    Vector2 localPosition = knobPosition - _position;
    if (localPosition.magnitude > Radius) {
      localPosition = localPosition.normalized * Radius;
    }
    _knob.anchoredPosition = localPosition;
  }

  public Vector2 GetDirections() {
    Vector2 directions = new Vector2(_knob.anchoredPosition.x / Radius, 
                                     _knob.anchoredPosition.y / Radius);
    return directions;
  }
}