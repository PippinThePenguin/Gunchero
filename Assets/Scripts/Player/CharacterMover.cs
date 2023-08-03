using System;
using UnityEngine;

//Moves player's character using physics, rotates character model.
[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : MonoBehaviour, IBehaviourController {
  public event Action<bool> OnStatusChange;

  public bool CanFire { get; private set; } = true;
  public PlayerInput Input;

  [SerializeField] private float _maxSpeed = 10f;
  [SerializeField] private Transform _charModel;

  private Rigidbody _charRb;
  
  private void Awake() {
    _charRb = GetComponent<Rigidbody>();   
  }

  private void OnEnable() {
    Init();
  }

  private void OnDisable() {
    Input.OnValueChange -= ChangeSpeed;
  }

  public void Init() {
    if (Input == null) {
      Input = FindObjectOfType<PlayerInput>();
    }
    Input.OnValueChange += ChangeSpeed;
  }

  private void ChangeSpeed(Vector2 newSpeed) {
    Vector3 velocity = new Vector3(newSpeed.x * _maxSpeed, 0f, newSpeed.y * _maxSpeed);
    _charRb.velocity = velocity;
    bool hasStoped = velocity == Vector3.zero;
    if (!hasStoped) {
      _charModel.rotation = Quaternion.LookRotation(velocity);
    }
    if (hasStoped != CanFire) {
      CanFire = hasStoped;
      OnStatusChange?.Invoke(CanFire);
    }
  }
}
