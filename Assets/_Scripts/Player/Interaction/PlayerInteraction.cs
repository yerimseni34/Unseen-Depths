using UnityEngine;
using TMPro;


public class PlayerInteraction : MonoBehaviour
{

    public Camera mainCamera;
    public float interactionDistance = 2f;

    public GameObject InteractionUI;
    public TextMeshProUGUI interactionText;

    private void Update()
    {
        InteractionRay();
    }

    private void InteractionRay()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }

        InteractionUI.SetActive(hitSomething);
    }


}
