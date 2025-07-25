using UnityEngine;

public class FireTorch : MonoBehaviour
{

    public float torchMaxDuration;
    public float torchDuration;

    public void RelightTorch()
    {
        torchDuration = torchMaxDuration;
    }


}
