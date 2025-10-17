using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{

    [SerializeField] CharacterSystem cs;

    [SerializeField] bool isSuctionActive;

    [SerializeField] Rigidbody rb;

    [SerializeField] Transform vacuumPoint; // The position coins should be sucked toward
    [SerializeField] float suckForce = 20f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float collectDistance = 0.5f;
    private Vector3 startScale; // Store initial scale
    private float initialDistance;

    [SerializeField] Transform ReturnItemPoint;

    [SerializeField] LorneScript lorneScript;
    [SerializeField] LotestScript lotestScript;
    [SerializeField] ZekeScript zekeScript;
    [SerializeField] NomiraScript nomiraScript;
    [SerializeField] TutorialScript tutorialScript;
    [SerializeField] IshizuScript ishizuScript;

    public static ItemSystem instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        nomiraScript = NomiraScript.instance;
        tutorialScript = TutorialScript.instance;
        ishizuScript = IshizuScript.instance;
    }
    private void FixedUpdate()
    {
        if (!isSuctionActive) return;

        if (rb != null)
        {
            // Turn off gravity and collider while getting sucked
            rb.useGravity = false;
            //Collider col = rb.GetComponent<Collider>(); //OLD
            //if (col != null) col.enabled = false; //OLD
            
            //new
            foreach (Collider col in rb.GetComponents<Collider>())
            {
                col.enabled = false;
            }

            Vector3 direction = (cs.currentCharacterObject.transform.position - rb.transform.position).normalized;
            rb.AddForce(direction * suckForce, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

            // Shrink the object as it moves towards the vacuum point
            float distance = Vector3.Distance(rb.transform.position, cs.currentCharacterObject.transform.position);
            float shrinkFactor = Mathf.Clamp01(distance / initialDistance); // Normalize shrink between 1 and 0
            rb.transform.localScale = startScale * shrinkFactor; // Apply scale reduction
        }

        // Check if close enough to be collected
        if (Vector3.Distance(rb.transform.position, cs.currentCharacterObject.transform.position) < collectDistance)
        {
            CollectItem(rb.gameObject);
        }
    }

    public void GetItemRb(GameObject item)
    {
        rb = item.GetComponent<Rigidbody>();
        startScale = rb.transform.localScale; // Store initial scale

        initialDistance = Vector3.Distance(rb.transform.position, cs.currentCharacterObject.transform.position); // Get starting distance
        isSuctionActive = true;
    }

    private void CollectItem(GameObject itemObject)
    {
        isSuctionActive = false;

        //Destroy the quest
        itemObject.GetComponent<ItemFloorScript>().ResetItemWhenCollected();
        itemObject.SetActive(false);

        ItemGlowOff();

    }

    public void ReturnItem(GameObject item)
    {
        item.SetActive(true);
        //item.transform.position = ReturnItemPoint.position;
    }

    public void ItemGlow()
    {
        //Debug.Log("glow");
        if (!LorneScript.instance.partOneComplete)
        {
            if(cs.currentCharacter.characterName == "Lorne")
            {
                lorneScript.purplePackageGlow.SetActive(true);
                lorneScript.blackPackageGlow.SetActive(true);
            }
        }
        else
        {
            if (cs.currentCharacter.characterName == "Lorne")
            {
                lorneScript.yarnGlow.SetActive(true);
            }
        }

        if (cs.currentCharacter.characterName == "Lotest Altall")
        {
            lotestScript.greenGlow.SetActive(true);
            lotestScript.velvetGlow.SetActive(true);
        }

        if (cs.currentCharacter.characterName == "Zeke")
        {
            zekeScript.raspberriesGlow.SetActive(true);
            zekeScript.magicMushroomGlow.SetActive(true);
            zekeScript.magicMushroomPotGlow.SetActive(true);
            zekeScript.lornePotionGlow.SetActive(true);
        }

        if (cs.currentCharacter.characterName == "Nomira")
        {
            nomiraScript.druidStaffGlow.SetActive(true);
            nomiraScript.divineStaffGlow.SetActive(true);
            nomiraScript.cosmicStaffGlow.SetActive(true);

            if (cs.currentCharacter.choseQuestB) //if its the golem quest
            {
                nomiraScript.weaponGlow.SetActive(true);
            }

        }
        if (cs.currentCharacter.characterName == "Josie")
        {
            tutorialScript.goldBundleGlow.SetActive(true);
        }

        if (cs.currentCharacter.characterName == "Ishizu")
        {
            ishizuScript.redHerbGlow.SetActive(true);
            ishizuScript.tealHerbGlow.SetActive(true);
        }

        if(cs.currentCharacter.characterName == "Achilles")
        {
            AchillesScript.instance.coinGlow.SetActive(true);
        }
    }

    public void ItemGlowOff()
    {
        //Lorne
        if(cs.currentCharacter.characterName == "Lorne")
        {
            lorneScript.purplePackageGlow.SetActive(false);
            lorneScript.blackPackageGlow.SetActive(false);
            lorneScript.yarnGlow.SetActive(false);
        }


        //Lotest
        if(cs.currentCharacter.characterName == "Lotest Altall")
        {
            lotestScript.greenGlow.SetActive(false);
            lotestScript.velvetGlow.SetActive(false);
        }


        //zeke
        if (cs.currentCharacter.characterName == "Zeke")
        {
            zekeScript.raspberriesGlow.SetActive(false);
            zekeScript.magicMushroomGlow.SetActive(false);
            zekeScript.magicMushroomPotGlow.SetActive(false);
            zekeScript.lornePotionGlow.SetActive(false);
        }


        if (cs.currentCharacter.characterName == "Nomira")
        {
            nomiraScript.druidStaffGlow.SetActive(false);
            nomiraScript.divineStaffGlow.SetActive(false);
            nomiraScript.cosmicStaffGlow.SetActive(false);
            nomiraScript.weaponGlow.SetActive(false);
        }
        if (cs.currentCharacter.characterName == "Josie")
        {
            tutorialScript.goldBundleGlow.SetActive(false);
        }
        if (cs.currentCharacter.characterName == "Ishizu")
        {
            ishizuScript.redHerbGlow.SetActive(false);
            ishizuScript.tealHerbGlow.SetActive(false);
        }
        if (cs.currentCharacter.characterName == "Achilles")
        {
            AchillesScript.instance.coinGlow.SetActive(false);
        }
    }
}
