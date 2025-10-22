using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CoinAudio : MonoBehaviour
{
    [Tooltip("Sound to play when the coin hits something.")]
    public AudioClip collisionClip;

    [Tooltip("Minimum speed required to play the sound.")]
    public float minImpactVelocity = 1f;

    private float lastSoundTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastSoundTime < 0.15f) return; // prevent spam
        lastSoundTime = Time.time;

        float impactStrength = collision.relativeVelocity.magnitude;

        if (impactStrength >= minImpactVelocity && collisionClip != null)
        {
            float volume = Mathf.Clamp01(impactStrength / 10f);
            CoinSoundManager.Instance.PlayCoinSound(collisionClip, volume);
        }
    }
}

