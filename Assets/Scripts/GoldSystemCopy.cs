using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI.Table;

public class GoldSystemCopy : MonoBehaviour
{

    public int goldAmount;
    [SerializeField] TMP_Text goldText;
    [SerializeField] GameObject goldUI;
    [SerializeField] GameObject goldDrawer;
    [SerializeField] Vector3 goldDrawerStartPos;
    [SerializeField] float drawerSpeed;

    [SerializeField] GameObject goldCoin;
    public List<GameObject> coins;
    [SerializeField] Transform coinSpawnPos;

    //FOR TUTORIAL
    public bool addedGold;

    private void Start()
    {
        goldDrawerStartPos = transform.position;
    }

    public void PressedDown()
    {
        if (goldAmount > 0) //cant go below 0
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
        if (goldAmount < 50) //cap at 99
        {
            goldAmount++;
            goldText.text = goldAmount.ToString();
            SpawnGold();
        }
    }

    public void OpenGoldDrawer()
    {
        goldDrawer.transform.DOMove(goldDrawerStartPos + new Vector3(0, 0, -1.5f), drawerSpeed);
        goldUI.SetActive(true);
    }

    public void CloseGoldDrawer()
    {
        goldDrawer.transform.DOMove(goldDrawerStartPos, drawerSpeed);
        goldUI.SetActive(false);
    }

    public void SpawnGold()
    {
        GameObject coin = Instantiate(goldCoin);
        float force = Random.Range(-1.5f, 1.5f);
        coin.GetComponent<Rigidbody>().AddForce((Vector3.right * force) + Vector3.up * 1.5f, ForceMode.Impulse);

        coins.Add(coin);

        Debug.Log(coins.Count);
    }

    public void RemoveGold()
    {
        Destroy(coins[coins.Count - 1]);
        coins.RemoveAt(coins.Count - 1);
        Debug.Log(coins.Count);
    }
}
