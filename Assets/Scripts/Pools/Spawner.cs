using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//Creates pools for objects in Infos array, distributes objects
public class Spawner : MonoBehaviour {
  [Serializable]
  public struct InfoForPool {
    public GameObject Prefab;
    public string Name;
    public int PoolSize;
  }

  public static Spawner Instance;
  public InfoForPool[] PoolInfos;

  private Dictionary<GameObject, Pool> _poolByPrefab;
  private Dictionary<string, Pool> _poolByName;

  private void Awake() {
    if (Instance != null) {
      Debug.LogWarning("Multipe Spawners!");
      PoolInfos = PoolInfos.Concat(Instance.PoolInfos).ToArray();
      Destroy(Instance);
    }
    Instance = this;
    CreatePools();    
  }

  private void CreatePools() {
    _poolByName = new Dictionary<string, Pool>();
    _poolByPrefab = new Dictionary<GameObject, Pool>();
    foreach (var info in PoolInfos) {
      var infoForDict = info;
      if (info.Prefab == null) {
        continue;
      }
      if (info.Name == "") {
        infoForDict.Name = info.Prefab.name;
      }
      if (info.PoolSize == 0) {
        infoForDict.PoolSize = 2;
      }

      Pool newPool = new Pool(infoForDict.Prefab, infoForDict.PoolSize, transform);
      _poolByName.Add(infoForDict.Name, newPool);
      _poolByPrefab.Add(infoForDict.Prefab, newPool);
    }
  }

  public Transform Spawn(GameObject obj) {
    Transform newObject = null;
    if (_poolByPrefab.ContainsKey(obj)) {
      newObject = _poolByPrefab[obj].Pull();
    } else {
      newObject = Instantiate(obj).transform;
    }
    return newObject;
  }

  public Transform Spawn(GameObject obj, Vector3 position, Quaternion rotation) {
    Transform newObject = Spawn(obj);
    newObject.position = position;
    newObject.rotation = rotation;
    return newObject;
  }

  public Transform Spawn(string name) {
    Transform newObject = null;
    if (_poolByName.ContainsKey(name)) {
      newObject = _poolByName[name].Pull();
    }
    return newObject;
  }

  public Transform Spawn(string name, Vector3 position, Quaternion rotation) {
    Transform newObject = Spawn(name);
    if (newObject != null) {
      newObject.position = position;
      newObject.rotation = rotation;
    }    
    return newObject;
  }
}
