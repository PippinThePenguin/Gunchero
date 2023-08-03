using System;
using UnityEngine;

//Plays effects on enemy death and on bullet hit
public class ParticlesManager : MonoBehaviour {
  private Action<Transform> PlayDeath;
  private Action<Vector3> PlayHit;

  private void OnEnable() {
    PlayDeath = (enemy) => PlayParticles(enemy.position, "DeathParticle");
    PlayHit = (position) => PlayParticles(position, "HitParticle");
    LevelFlow.OnEnemyDeath += PlayDeath;
    LevelFlow.OnBulletHit += PlayHit;
  }

  private void OnDisable() {
    LevelFlow.OnEnemyDeath -= PlayDeath;
    LevelFlow.OnBulletHit -= PlayHit;
  }

  private void PlayParticles(Vector3 position, string name) {
    Transform particle = Spawner.Instance.Spawn(name, position, transform.rotation);
    if (particle != null) {
      particle.gameObject.SetActive(true);
    }
  }
}
