using System;
using UnityEngine;
using UnityEngine.UI;

public class interactableObjectUI : MonoBehaviour
{
    public event Action<Iinteractable> FoundInteractObject;

    private Ray ray;

    [HideInInspector]
    public Image dot;

    public LayerMask mask;

    [SerializeField]
    [Range(0f, 10f)]
    private float rayLength;

    private Iinteractable lastInteracted;

    [SerializeField]
    private Camera thisCamera;

    [SerializeField]
    private Camera firstpersonCamera;

    [SerializeField]
    private Camera SittingCamera;


    private void Awake()
    {
        thisCamera = Camera.main;
    }

    private void Start()
    {
        dot = GetComponent<Image>();
        dot.enabled = false;
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out var hitInfo, rayLength, mask))
        {
            if (hitInfo.collider.GetComponent<Iinteractable>() != null)
            {
                dot.enabled = true;
                FoundInteractObject?.Invoke(hitInfo.collider.GetComponent<Iinteractable>());
            }
            else
            {
                dot.enabled = false;
                FoundInteractObject?.Invoke(null);
            }
        }
        else
        {
            dot.enabled = false;
            FoundInteractObject?.Invoke(null);
        }
    }

    public void ChangeCamera(Camera currentCamera)
    {
        thisCamera = currentCamera;
    }
}