using Assets.Game.Code.Static;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Code.Utils
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _shotParticle, _bloodParticle;


        private void Start()
        {
            Observer.Instance.OnWeaponShotHandler += PlayShotParticle;
            Observer.Instance.OnEnemyHitHandler += PlayBloodParticle;
        }

        private void PlayBloodParticle(Vector3 at, Vector3 direction)
        {
            _bloodParticle.transform.DORotate(direction.normalized, 0f);
            PlayParticle(_bloodParticle, at);
        }

        private void PlayParticle(ParticleSystem particle, Vector3 at)
        {
            particle.transform.position = at;
            particle.gameObject.SetActive(true);
            particle.Play();
        }

        private void PlayShotParticle(Vector3 at)
        {
            PlayParticle(_shotParticle, at);
        }

        private void OnDestroy()
        {
            Observer.Instance.OnWeaponShotHandler -= PlayShotParticle;
            Observer.Instance.OnEnemyHitHandler -= PlayBloodParticle;
        }
    }
}