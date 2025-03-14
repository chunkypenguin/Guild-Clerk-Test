using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DaySystem : MonoBehaviour
{
    int dayCount = 1;
    public Image targetImage; // Assign this in the Inspector
    public float fadeDuration = 1.5f; // Duration of fade effect
    [SerializeField] GameObject dayOneTextObject;
    [SerializeField] GameObject dayTwoTextObject;

    bool canStartNextDay;

    [SerializeField] CharacterSystem cs;

    [SerializeField] GameObject[] returnItems;


    private void Start()
    {
        dayCount = 1;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canStartNextDay)
        {
            canStartNextDay = false;
            NewDay();
        }
    }

    public void EndDay()
    {
        targetImage.gameObject.SetActive(true);
        if (targetImage != null)
        {
            // Fade in effect
            targetImage.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                dayCount++;
                if(dayCount == 2)
                {
                    dayOneTextObject.SetActive(true);
                    cs.D2 = true;
                    cs.D1 = false;
                }
                else if (dayCount == 3)
                {
                    dayTwoTextObject.SetActive(true);
                    cs.D3 = true;
                    cs.D2 = false;
                    cs.D1 = false;
                }

                canStartNextDay = true;
                cs.currentCharacterObject.GetComponent<MoveCharacter>().MoveEndDay();

                //remove return items
                foreach (GameObject obj in returnItems)
                {
                    obj.SetActive(false);
                }

            });
        }
        else
        {
            Debug.LogError("No Image assigned to FadeImage script!");
        }
    }

    public void NewDay()
    {
        dayOneTextObject.SetActive(false);
        dayTwoTextObject.SetActive(false);

        if (targetImage != null)
        {
            // Fade in effect
            targetImage.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                
                targetImage.gameObject.SetActive(false);
                cs.StartNewCharacter();

            });
        }
        else
        {
            Debug.LogError("No Image assigned to FadeImage script!");
        }

    }
}
