using UnityEngine;

public class TorchCollider : MonoBehaviour
{

    [SerializeField] private Transform torchCollider;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform mainCameraPos;

    private void Update()
    {
        torchCollider.position = playerPos.position;
        torchCollider.rotation = mainCameraPos.rotation;
    }

}
