using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private float delayBetweenSpawns = 0.5f;

    [SerializeField] GameObject firework;
    [SerializeField] List<Transform> fireworksSpawners;
    [SerializeField] AudioSource fireworkSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //FireworksStuff();
        }
    }

    private IEnumerator SpawnParticlesSequentially()
    {
        foreach (Transform spawnpoint in fireworksSpawners)
        {
            //particleObj.SetActive(true);

            Instantiate(firework, spawnpoint);
            fireworkSound.pitch = Random.Range(0.9f, 1.15f);
            fireworkSound.Play();   
            //// If Play On Awake is off, manually play:
            //var ps = particleObj.GetComponent<ParticleSystem>();
            //if (ps != null && !ps.main.playOnAwake)
            //    ps.Play();

            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }

    public void FireworksStuff()
    {
        StartCoroutine(SpawnParticlesSequentially());
    }
}
