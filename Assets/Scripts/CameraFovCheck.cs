using UnityEngine;

//Checks if FovPoints is all on screen, enlarges FOV if not
public class CameraFovCheck : MonoBehaviour {
  public Transform[] FovPoints;
  
  void Update() {
    CheckView();
  }

  void CheckView() {
    bool isOk = true;
    foreach (var fov in FovPoints) {
      Vector3 vpPos = Camera.main.WorldToViewportPoint(fov.position);
      if (!(vpPos.x >= 0f && vpPos.x <= 1f && vpPos.y >= 0f && vpPos.y <= 1f && vpPos.z > 0f)) {
        isOk = false;
      }
    }
    if (!isOk) {
      Camera.main.fieldOfView += 1f;
    }
  }
}
