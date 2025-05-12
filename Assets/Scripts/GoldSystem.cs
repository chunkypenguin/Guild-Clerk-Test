using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GoldSystem : MonoBehaviour
{
    public int goldAmount;
    [SerializeField] TMP_Text goldText;
    [SerializeField] GameObject goldUI;
    [SerializeField] GameObject goldDrawer;
    [SerializeField] Vector3 goldDrawerStartPos;
    [SerializeField] float drawerSpeed;

    [SerializeField] GameObject goldCoin;
    public List<GameObject> coins = new List<GameObject>(); // Ensure list is initialized
    [SerializeField] Transform coinSpawnPos;

    [SerializeField] CharacterSystem cs;
    // FOR TUTORIAL
    public bool addedGold;

    // VACUUM SYSTEM
    public bool isSuctionActive = false;
    [SerializeField] Transform vacuumPoint; // The position coins should be sucked toward
    [SerializeField] float suckForce = 20f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float collectDistance = 0.5f;

    [SerializeField] movecam mc;

    public static GoldSystem instance;

    private void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        goldDrawerStartPos = transform.position;
    }

    private void Update()
    {
        // Start suction when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && coins.Count > 0)
        {
            //isSuctionActive = true;
        }

        // Stop suction when all coins are collected
        if (isSuctionActive && coins.Count == 0)
        {
            isSuctionActive = false;
            Debug.Log("All coins collected! Suction stopped.");
        }
    }

    private void FixedUpdate()
    {
        if (!isSuctionActive) return;

        // Move all coins toward the vacuum point
        for (int i = coins.Count - 1; i >= 0; i--)
        {
            GameObject coin = coins[i];
            if (coin == null) continue; // Skip if coin was already destroyed

            Rigidbody rb = coin.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Turn off gravity and collider while getting sucked
                rb.useGravity = false;
                Collider col = coin.GetComponent<Collider>();
                if (col != null) col.enabled = false;

                Vector3 direction = (cs.currentCharacterObject.transform.position - coin.transform.position).normalized;
                rb.AddForce(direction * suckForce, ForceMode.Acceleration);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

            // Check if close enough to be collected
            if (Vector3.Distance(coin.transform.position, cs.currentCharacterObject.transform.position) < collectDistance)
            {
                CollectCoin(coin);
            }
        }
    }

    void CollectCoin(GameObject coin)
    {
        Debug.Log(coin.name + " collected!");

        // Remove from the list
        coins.Remove(coin);

        goldAmount--;
        goldText.text = goldAmount.ToString();

        // Destroy the coin
        Destroy(coin);
    }

    public void PressedDown()
    {
        if (goldAmount > 0) // Can't go below 0
        {
            goldAmount--;
            goldText.text = goldAmount.ToString();
            RemoveGold();
        }
    }

    public void PressedUp()
    {
        if (!addedGold)
        {
            addedGold = true;
        }
        if (goldAmount < 50) // Cap at 50
        {
            goldAmount++;
            goldText.text = goldAmount.ToString();
            SpawnGold();
        }
    }

    public void OpenGoldDrawer()
    {
        Debug.Log("open drawer");
        goldDrawer.transform.DOMove(goldDrawerStartPos + new Vector3(0, 0, -1.5f), drawerSpeed);
        goldUI.SetActive(true);
    }

    public void CloseGoldDrawer()
    {
        goldDrawer.transform.DOMove(goldDrawerStartPos, drawerSpeed);
        goldUI.SetActive(false);
        mc.drawerOpen = false;
    }

    public void SpawnGold()
    {
        GameObject coin = Instantiate(goldCoin, coinSpawnPos.position, Quaternion.identity);
        float force = Random.Range(-1.5f, 1.5f);
        Rigidbody rb = coin.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce((Vector3.right * force) + Vector3.up * 1.5f, ForceMode.Impulse);
        }

        coins.Add(coin);
        Debug.Log(coins.Count);
    }

    public void RemoveGold()
    {
        if (coins.Count > 0)
        {
            GameObject lastCoin = coins[coins.Count - 1];
            coins.RemoveAt(coins.Count - 1);
            Destroy(lastCoin);
            Debug.Log(coins.Count);
        }
    }

    public void GoldDrawerDisable()
    {
        gameObject.GetComponent<MeshCollider>().enabled = false;
        Invoke(nameof(GoldDrawerEnable), 0.25f);
    }

    public void GoldDrawerEnable()
    {
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    public void GiveBackGold()
    {
        for (int i = 0; i < 5; i++) // Calls the function 5 times
        {
            PressedUp();
        }
    }
}
