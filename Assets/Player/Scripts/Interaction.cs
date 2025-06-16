using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 2f;
    public LayerMask interactableLayer;
    public Transform interactPoint;

    private InputSystem_Actions controls;
    private bool interactPressed;

    private void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Interact.performed += ctx => interactPressed = true;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        DrawDebugRay();

        if (interactPressed)
        {
            TryInteract();
            interactPressed = false;
        }
    }

    private void TryInteract()
    {
        // Determine direction manually based on player scale
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(interactPoint.position, direction, interactRange, interactableLayer);

        Debug.Log("Interact Shot");

        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
            else
            {
                Debug.Log("Hit something without IInteractable component.");
            }
        }
        else
        {
            Debug.Log("Nothing hit by raycast.");
        }
    }

    private void DrawDebugRay()
    {
        Vector3 direction = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        Debug.DrawRay(interactPoint.position, direction * interactRange, Color.yellow, 0.1f); // Draw for 0.1 seconds
    }

    private void OnDrawGizmosSelected()
    {
        if (interactPoint != null)
        {
            Gizmos.color = Color.cyan;
            Vector3 direction = Application.isPlaying && transform.localScale.x < 0 ? Vector3.left : Vector3.right;
            Gizmos.DrawLine(interactPoint.position, interactPoint.position + direction * interactRange);
        }
    }
}