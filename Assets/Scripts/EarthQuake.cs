using UnityEngine;
using DG.Tweening;

public class EarthQuake : MonoBehaviour
{
    [SerializeField] float duration = 2f;    // How long the shake lasts
    [SerializeField] float strength = 0.1f;  // Max shake offset per axis
    [SerializeField] int vibrato = 10;       // How many shakes during the duration
    [SerializeField] float randomness = 90f; // How random the shake feels (0 = uniform)

    [SerializeField] GameObject cam;

    public void Shake()
    {
        // Shake the object's position
        transform.DOShakePosition(duration, strength, vibrato, randomness)
                 .SetTarget(transform);
        cam.transform.DOShakePosition(duration, strength, vibrato, randomness)
         .SetTarget(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shake();
        }
    }
}
