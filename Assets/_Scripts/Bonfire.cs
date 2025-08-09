using System.Collections;
using UnityEngine;

public class Bonfire : MonoBehaviour, IInteractable
{

    public bool awakeLighting;

    [Header("Light Things")]
    [SerializeField] private ParticleSystem BoneFireParticleSystem;

    public string GetDescription()
    {
        if (awakeLighting)
        {
            return "Relight the torch";
        }
        else
        {
            return "Relight the boneFire";
        }
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
                if (awakeLighting)
                {
                    torch.RelightTheTorch();
                }
                else if (!awakeLighting && torch.torchDuration > 0)
                {
                    awakeLighting = true;
                    RelightTheBoneFire();
                }
            }
        }
    }

    private void Awake()
    {
        if (awakeLighting)
        {
            BoneFireParticleSystem.Play();
        }
        else
        {
            var emission = BoneFireParticleSystem.emission;
            emission.rateOverTime = 0f;
        }
    }


    private void RelightTheBoneFire()
    {
        BoneFireParticleSystem.Play();
        StartCoroutine(IncreaseEmissionOverTime());
    }


    private IEnumerator IncreaseEmissionOverTime()
    {
        var emission = BoneFireParticleSystem.emission;
        float currentRate = 0f;
        float targetRate = 50f; // hedef emission rate (ayarına göre değiştir)

        while (currentRate < targetRate)
        {
            currentRate += Time.deltaTime * 10f; // ne kadar hızlı artsın
            emission.rateOverTime = currentRate;
            yield return null;
        }

        // Emin olmak için sabitle
        emission.rateOverTime = targetRate;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(10, 10, 10));
    }

}
