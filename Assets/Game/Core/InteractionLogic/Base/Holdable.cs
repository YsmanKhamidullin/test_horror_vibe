using System.Collections;
using Game.Core.Architecture;
using Game.Core.Architecture.Services;
using Game.Core.Canvas;
using Game.Core.Canvas.Base;
using Game.Core.Player;
using UnityEngine;

namespace Game.Core.InteractItems
{
    public abstract class Holdable : MonoBehaviour
    {
        public AudioClip[] sfxSoundsArray;

        protected AudioSource source;

        private const float delayInThrowSound = 0.1f;

        public Rigidbody rBody;

        protected PlayerController playerController;

        protected bool inUse;

        private bool isInteractible;
        protected CanvasWindow _canvasWindow;

        public bool IsInUse()
        {
            return inUse;
        }

        protected async virtual void Start()
        {
            _canvasWindow = Project.ProjectContext.PlayerController.CanvasWindow;
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            source.volume = 1f;
            source.spatialBlend = 1f;
            rBody = GetComponent<Rigidbody>();
            if (rBody != null)
            {
                rBody.isKinematic = true;
            }

            playerController = Project.ProjectContext.PlayerController;
        }

        protected IEnumerator ThrowSound()
        {
            source = GetComponent<AudioSource>();
            yield return new WaitForSeconds(0.1f);
            if (sfxSoundsArray.Length != 0)
            {
                source.PlayOneShot(sfxSoundsArray[Random.Range(0, sfxSoundsArray.Length)], 1f);
            }
        }

        public virtual void GoToPosition(Transform parentTransform)
        {
            if ((object)playerController == null)
            {
                playerController = Project.ProjectContext.PlayerController;
            }

            base.gameObject.SetActive(value: true);
            base.transform.parent = parentTransform;
            base.transform.localPosition = Vector3.zero;
            base.transform.localRotation = Quaternion.Euler(Vector3.zero);
            if ((object)rBody == null)
            {
                rBody = GetComponent<Rigidbody>();
            }

            if (rBody == null)
            {
                rBody = base.gameObject.AddComponent<Rigidbody>();
            }

            rBody.isKinematic = true;
        }

        public void SetPos(Vector3 position)
        {
            rBody.isKinematic = true;
            transform.position = position;
            transform.rotation = Quaternion.identity;
        }

        public virtual void Throw(Transform throwDirection, float throwSpeed = 300)
        {
            base.transform.parent = null;
            rBody.isKinematic = false;
            rBody.AddForce((throwDirection.forward + throwDirection.up / 2.5f) * throwSpeed);
            StartCoroutine(ThrowSound());
        }

        public virtual void SetForUse()
        {
            inUse = true;
            playerController.RemoveHandObject();
        }

        public void SetInteractable(bool value)
        {
            isInteractible = value;
            base.gameObject.layer =
                (value ? LayerMask.NameToLayer("Default") : LayerMask.NameToLayer("Ignore Raycast"));
        }
    }
}