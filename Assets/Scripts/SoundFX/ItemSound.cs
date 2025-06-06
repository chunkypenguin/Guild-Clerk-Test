using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [Tooltip("Sound to play when the object hits something.")]
    public AudioClip collisionClip;

    [Tooltip("Minimum speed required to play the sound.")]
    public float minImpactVelocity = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactStrength = collision.relativeVelocity.magnitude;

        if (impactStrength >= minImpactVelocity && collisionClip != null)
        {
            float volume = Mathf.Clamp01(impactStrength / 10f);
            audioSource.PlayOneShot(collisionClip, volume);
        }
    }
}