using System.Collections.Generic;
using UnityEngine;

public class Pool {
  public int MaxSize = 100;
  public GameObject _prefab;
  private Transform _parent;
  private Stack<Transform> _inactiveObjects;
  
  public Pool(GameObject prefab, int startSize, Transform parent = null) {
    if (!prefab.GetComponent<ObjectOfPool>()) {
      throw new MissingComponentException(nameof(ObjectOfPool));
    }
    _prefab = prefab;
    _parent = parent;
    _inactiveObjects = new Stack<Transform>();
    for (int i = 0; i < startSize; i++) {
      Expand();
    }
  }

  public bool TryPush(ObjectOfPool obj) {
    if (_inactiveObjects.Count >= MaxSize) {
      return false;
    }
    obj.transform.parent = _parent;
    obj.transform.localPosition = Vector3.zero;
    _inactiveObjects.Push(obj.transform);
    return true;
  }

  public Transform Pull() {
    if (_inactiveObjects.Count == 0) {
      Expand();
    }
    return _inactiveObjects.Pop();
  }

  private void Expand() {
    GameObject spawnObject = GameObject.Instantiate(_prefab, _parent);
    spawnObject.GetComponent<ObjectOfPool>().Imprint(this);
    spawnObject.SetActive(false);
  }
}
