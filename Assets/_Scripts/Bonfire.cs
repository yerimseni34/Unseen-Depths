using UnityEngine;

public class Bonfire : MonoBehaviour, IInteractable
{
    public string GetDescription()
    {
        return "Relight the torch";
    }

    public void Interact()
    {
        Debug.Log(Random.Range(1, 100));
    }
}
