using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture;
using Game.Core.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.InteractionLogic
{
    public class CoffeeMachine : MonoBehaviour
    {
        [SerializeField]
        private List<CollisionListener> _collisionListeners;

        [FormerlySerializedAs("_coffeeCupPos")]
        [SerializeField]
        private Transform _coffeeCupPosTransform;

        [SerializeField]
        private ParticleSystem _coffeeParticleSystem;

        private CoffeeCup _coffeeCup;
        private PlayerController _playerController;

        private void Awake()
        {
            foreach (var c in _collisionListeners)
            {
                c.OnCollisionEnterEvent += TryPutCoffeeCup;
            }
        }

        private void Start()
        {
            _playerController = Project.ProjectContext.PlayerController;
        }

        private void OnDestroy()
        {
            foreach (var c in _collisionListeners)
            {
                c.OnCollisionEnterEvent -= TryPutCoffeeCup;
            }
        }

        private void TryPutCoffeeCup(Collider c, Collision collision)
        {
            if (_coffeeCup != null)
            {
                return;
            }

            if (!collision.collider.gameObject.TryGetComponent(out _coffeeCup))
            {
                return;
            }

            if (_coffeeCup.IsFilled)
            {
                return;
            }

            FillCoffee(_coffeeCup).Forget();
        }

        private async UniTask FillCoffee(CoffeeCup coffeeCup)
        {
            _coffeeCup = coffeeCup;
            _coffeeCup.SetInteractable(false);

            coffeeCup.SetPos(_coffeeCupPosTransform.position);
            _coffeeParticleSystem.Play();
            coffeeCup.CoffeeAnimator.gameObject.SetActive(true);
            await UniTask.WaitForSeconds(5f);
            _coffeeParticleSystem.Stop();
            _coffeeCup.FilledWithCoffee();
            await UniTask.WaitForSeconds(0.5f);
            
            await UniTask.WaitUntil(() => _coffeeCup.IsCapEquiped);
            _coffeeCup.SetInteractable(true);
            await UniTask.WaitUntil(() => _playerController.GetHoldingObject() == coffeeCup);
            _coffeeCup = null;
        }
    }
}