using UnityEngine;

//Stores information about pool of origin for returning
public class ObjectOfPool : MonoBehaviour {
  private Pool _myPool;

  private void OnDisable() {
    if (_myPool == null || !_myPool.TryPush(this)){
      Destroy(gameObject);
    }
  }

  public void Imprint(Pool parentPool) {
    _myPool = parentPool;    
  }
}
