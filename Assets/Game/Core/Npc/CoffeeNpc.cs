using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture;
using Game.Core.Architecture.Services;
using Game.Core.Canvas;
using Game.Core.Canvas.Base;
using Game.Core.Player;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Core.Npc
{
    public class CoffeeNpc : Npc, Iinteractable
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private List<Transform> _walkPoints;

        [SerializeField]
        private EntranceDoor _entranceDoor;

        [SerializeField]
        private PlayerController _playerController;

        private bool _isPlayerInteracted;
        private CanvasWindow _canvasWindow;

        private async void Start()
        {
            var gw = await Project.Get<GameWindowsService>();
            _canvasWindow = gw.Get<CanvasWindow>();
            StartSequence().Forget();
        }

        [Button]
        private void RestartSequence()
        {
            StartSequence().Forget();
        }
        
        private async UniTask StartSequence()
        {
            // Wait for the entrance door to open and NPC to move to first point
            await UniTask.WaitForSeconds(2f);

            await MoveToPoint(_walkPoints[0]);
            _entranceDoor.Open();
            await UniTask.WaitForSeconds(0.7f);

            // Move to second point and close the door
            await MoveToPoint(_walkPoints[1]);
            _entranceDoor.Close();
            await UniTask.WaitForSeconds(0.3f);

            // Move to final position
            await MoveToPoint(_walkPoints[2]);

            // Wait for player interaction
            await UniTask.WaitUntil(() => _isPlayerInteracted);
            Debug.Log("s");
            _isPlayerInteracted = false;

            await Project.ProjectContext.DialogueSequenceWrapper.StartSequence(GameResources._VisualNovel.D__01_Npc);
            await MoveToPoint(_walkPoints[3]);
            
            await RotateToPlayer();
        }

        private async UniTask MoveToPoint(Transform targetPoint)
        {
            var target = targetPoint.position;
            target.y = _navMeshAgent.transform.position.y;

            _navMeshAgent.SetDestination(target);
            await UniTask.WaitForEndOfFrame();
            _animator.SetBool("IsWalking", true);
            // Wait until we reach the destination
            var distance = Vector3.Distance(_navMeshAgent.transform.position, target);
            while (_navMeshAgent.pathPending || distance > 0.05f)
            {
                distance = Vector3.Distance(_navMeshAgent.transform.position, target);
                await UniTask.Yield();
            }

            _animator.SetBool("IsWalking", false);
        }

        private async UniTask RotateToPlayer()
        {
            var directionToPlayer = _playerController.transform.position - transform.position;
            directionToPlayer.y = 0;
            if (directionToPlayer != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(directionToPlayer);
                float rotationSpeed = 120f; // degrees per second
                
                while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
                {
                    transform.rotation = Quaternion.RotateTowards(
                        transform.rotation,
                        targetRotation,
                        rotationSpeed * Time.deltaTime
                    );
                    await UniTask.WaitForEndOfFrame();
                }
            }
        }

        public void Clicked(Action removedFromHand = null)
        {
            Debug.Log("Player clicked npc");
            _isPlayerInteracted = true;
        }

        private bool _isCollidedWithCoffee;

        public void OnCoffeeCapCollision()
        {
            if (_isCollidedWithCoffee)
            {
                return;
            }

            _isCollidedWithCoffee = true;
            _canvasWindow.MoneyUi.Show();
        }
    }
}