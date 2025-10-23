using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CoinFountain : MonoBehaviour
{
    [SerializeField] private RectTransform uiSpawnPoint;
    [SerializeField] private Image coinPrefab;
    [SerializeField] private Canvas canvas;

    public int goldGiven = 0;
    [SerializeField] private float delayBetweenCoins = 0.05f;
    [SerializeField] private Vector2 randomXRange = new Vector2(-50f, 50f);
    [SerializeField] private Vector2 randomYRange = new Vector2(80f, 150f);
    [SerializeField] private float jumpPower = 100f;      // Height of the arc
    [SerializeField] private int numJumps = 1;            // Usually 1 for a single arc
    [SerializeField] private float jumpDuration = 0.6f;
    [SerializeField] private float scaleDown = 0.5f;
    [SerializeField] private float fadeDuration = 0.3f;
    private bool spawnCoin;

    [SerializeField] int coinCount;
    [SerializeField] TMP_Text goldGivenText;

    public TMP_Text goldRequestedText;

    [SerializeField] AudioSource coinAudioSource;

    public static CoinFountain instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        spawnCoin = true;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            //ShootCoins();
        }
    }

    public void ShootCoins()
    {
        StartCoroutine(ShootCoinsCoroutine());
        if(goldGiven > 0)
        {
            coinAudioSource.Play();
        }

    }

    private IEnumerator ShootCoinsCoroutine()
    {
        for (int i = 0; i < goldGiven; i++)
        {
            coinCount++;
            goldGivenText.text = coinCount.ToString();

            if (spawnCoin)
            {
                coinAudioSource.pitch = Random.Range(0.8f, 1.2f);
                if (i == goldGiven - 1)
                {
                    coinAudioSource.pitch = 1.5f;
                }

                Image coin = Instantiate(coinPrefab, canvas.transform);
                coin.transform.position = uiSpawnPoint.position;
                //coin.transform.localScale = Vector3.one;
                coin.color = Color.white;

                float randomX = Random.Range(randomXRange.x, randomXRange.y);
                float randomY = Random.Range(randomYRange.x, randomYRange.y);
                Vector3 targetPos = coin.transform.position + new Vector3(randomX, randomY, 0);

                // Animate jump in an arc
                coin.transform.DOJump(targetPos, jumpPower, numJumps, jumpDuration).SetEase(Ease.OutQuad);

                // Scale down
                coin.transform.DOScale(scaleDown, jumpDuration).SetEase(Ease.OutQuad);

                // Fade out after jump
                coin.DOFade(0f, fadeDuration).SetDelay(jumpDuration).OnComplete(() =>
                {
                    Destroy(coin.gameObject);
                });

                spawnCoin = false;
            }
            else
            {
                spawnCoin = true;
            }

            yield return new WaitForSeconds(delayBetweenCoins);
        }

        coinAudioSource.Stop();

        //go to next dialogue
        ReviewManager.instance.GoldDialogueResults();
        ReviewManager.instance.ReviewInProgressCheck(); //turns on review in progress bool
        //dont skip dialogue after 
    }
}
