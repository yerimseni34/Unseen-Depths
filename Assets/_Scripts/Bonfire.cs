using UnityEngine;

public class Bonfire : MonoBehaviour, IInteractable
{

    public string GetDescription()
    {
        return "Relight the torch";
    }

    public void Interact()
    {
        Vector3 center = transform.position;
        Vector3 boxRadius = new Vector3(10, 10, 10);
        Quaternion orientation = Quaternion.identity;

        Collider[] colliders = Physics.OverlapBox(center, boxRadius, orientation);
        foreach (Collider col in colliders)
        {
            Torch torch = col.GetComponent<Torch>();
            if (torch != null)
            {
                torch.RelightTheTorch();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(10, 10, 10));
    }

}
