using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lightbug.GrabIt;

namespace HeneGames.DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        #region Singleton

        public static DialogueUI instance { get; private set; }

        private void Awake()
        {
            instance = this;
            //if (instance == null)
            //{
            //    instance = this;
            //    DontDestroyOnLoad(gameObject);
            //}
            //else
            //{
            //    Destroy(gameObject);
            //}

            //Hide dialogue and interaction UI at awake
            dialogueWindow.SetActive(false);
            interactionUI.SetActive(false);
        }

        #endregion

        private DialogueManager currentDialogueManager;
        private bool typing;
        private string currentMessage;
        private float startDialogueDelayTimer;

        [Header("References")]
        [SerializeField] private Image portrait;
        [SerializeField] public TextMeshProUGUI nameText;
        [SerializeField] public TextMeshProUGUI messageText;
        [SerializeField] private GameObject dialogueWindow;
        [SerializeField] private GameObject interactionUI;

        [Header("Day Stuff")]
        [SerializeField] TextMeshProUGUI dayText;

        [Header("Settings")]
        [SerializeField] private bool animateText = true;

        [Range(0.1f, 1f)]
        [SerializeField] public float textAnimationSpeed = 0.5f;

        [Header("Next sentence input")]
        public KeyCode actionInput = KeyCode.Space;

        //MY ADDITION
        [Header("Get Last Sentence")]
        public string lastMessage;

        [SerializeField] GoldSystem gs;

        int paraCount = 0;

        [SerializeField] AudioSource textAudio;

        private void Start()
        {
            gs = GoldSystem.instance;
        }

        private void Update()
        {
            //Delay timer
            if(startDialogueDelayTimer > 0f)
            {
                startDialogueDelayTimer -= Time.deltaTime;
            }

            InputUpdate();
        }

        public virtual void InputUpdate()
        {
            //Next dialogue input
            if (Input.GetKeyDown(actionInput) && !DaySystem.instance.endOfDay)
            {
                NextSentenceSoft();
            }
        }

        /// <summary>
        /// If a sentence is being written and this function is called, the sentence is completed instead of immediately moving to the next sentence.
        /// This function needs to be called twice if you want to switch to a new sentence.
        /// </summary>
        public void NextSentenceSoft()
        {
            if (startDialogueDelayTimer <= 0f)
            {
                if (!typing)
                {
                    NextSentenceHard();
                }
                else
                {
                    StopAllCoroutines();
                    typing = false;
                    messageText.text = currentMessage;

                    //NEW
                    messageText.maxVisibleCharacters = messageText.textInfo.characterCount;
                    Debug.Log("Complete Sentence Next Sentence");

                    //NEW CODE TO TRY AND STOP DIALOGUE SOUND
                    if (currentDialogueManager.audioSource != null)
                    {
                        currentDialogueManager.audioSource.Stop();
                    }

                }
            }
        }

        /// <summary>
        /// Even if a sentence is being written, with this function immediately moves to the next sentence.
        /// </summary>
        public void NextSentenceHard()
        {
            //Continue only if we have dialogue
            if (currentDialogueManager == null)
                return;

            //Tell the current dialogue manager to display the next sentence. This function also gives information if we are at the last sentence
            currentDialogueManager.NextSentence(out bool lastSentence);

            //If last sentence remove current dialogue manager
            if (lastSentence)
            {
                lastMessage = currentMessage;
                Debug.Log(lastMessage);
                currentDialogueManager = null;
                Debug.Log("dialogue end");
                gs.GoldDrawerDisable();
            }
        }

        public void StartDialogue(DialogueManager _dialogueManager)
        {
            //Delay timer
            startDialogueDelayTimer = 0.1f;

            //Store dialogue manager
            currentDialogueManager = _dialogueManager;

            //Start displaying dialogue
            currentDialogueManager.StartDialogue();

            Debug.Log("start dialogue");
        }

        public void ShowSentence(DialogueCharacter _dialogueCharacter, string _message)
        {
            StopAllCoroutines();

            dialogueWindow.SetActive(true);

            portrait.sprite = _dialogueCharacter.characterPhoto;
            nameText.text = _dialogueCharacter.characterName;
            currentMessage = _message;

            if (animateText)
            {
                StartCoroutine(WriteTextToTextmesh(_message, messageText));
            }
            else
            {
                messageText.text = _message;
                Debug.Log("Complete Sentence Show Sentence");
            }
        }

        public void ClearText()
        {
            Debug.Log("DiaUI-Clear Text");
            dialogueWindow.SetActive(false);
            movecam.instance.DrawerLockOut();//lock out drawer temporarliy to prevent misclicks
            CharacterSystem.instance.dialogueHistory.dialogueIsOn = false; //might fix texthistory bug
            MousePos3D.instance.dialogueOpen = false;

            DialogueBoxMouse.instance.hoveringDiaBox = false;
        }

        public void ShowInteractionUI(bool _value)
        {
            interactionUI.SetActive(_value);
        }

        public bool IsProcessingDialogue()
        {
            if(currentDialogueManager != null)
            {
                return true;
            }

            return false;
        }

        public bool IsTyping()
        {
            return typing;
        }

        public int CurrentDialogueSentenceLenght()
        {
            if (currentDialogueManager == null)
                return 0;

            return currentDialogueManager.CurrentSentenceLenght();
        }

        //ORIGNAL
        //public IEnumerator WriteTextToTextmesh(string _text, TextMeshProUGUI _textMeshObject)
        //{
        //    typing = true;

        //    _textMeshObject.text = "";
        //    char[] _letters = _text.ToCharArray();

        //    float _speed = 1f - textAnimationSpeed;

        //    foreach (char _letter in _letters)
        //    {
        //        _textMeshObject.text += _letter;

        //        if (_textMeshObject.text.Length == _letters.Length)
        //        {
        //            typing = false;
        //            //NEW CODE TO TRY AND STOP DIALOGUE SOUND
        //            if (currentDialogueManager.audioSource != null)
        //            {
        //                currentDialogueManager.audioSource.Stop();
        //            }

        //        }

        //        yield return new WaitForSeconds(0.1f * _speed);
        //    }
        //}

        //TEST FOR NEW DIALGOUE SCROLL
        public IEnumerator WriteTextToTextmesh(string _text, TextMeshProUGUI _textMeshObject)
        {
            typing = true;

            // Set the full text immediately (so TMP knows what to generate).
            _textMeshObject.text = _text;

            // Force TMP to generate mesh/character info *before* counting.
            _textMeshObject.ForceMeshUpdate();

            // Start with no characters visible.
            _textMeshObject.maxVisibleCharacters = 0;

            float _speed = 1f - textAnimationSpeed;

            int totalCharacters = _textMeshObject.textInfo.characterCount;

            for (int i = 0; i <= totalCharacters; i++)
            {
                _textMeshObject.maxVisibleCharacters = i;

                if (i == totalCharacters)
                {
                    typing = false;

                    // NEW CODE TO TRY AND STOP DIALOGUE SOUND
                    if (currentDialogueManager.audioSource != null)
                    {
                        currentDialogueManager.audioSource.Stop();
                    }
                }

                yield return new WaitForSeconds(0.1f * _speed);
            }
        }


        //TEST FOR END DAY
        public IEnumerator WriteTextToTextmeshDay(DayTexts[] day)
        {
            Debug.Log("Start Coroutine");

            //textAudio.Play();

            typing = true;

            day[DaySystem.instance.dayCount].ParaTexts[paraCount].ForceMeshUpdate();

            day[DaySystem.instance.dayCount].ParaTexts[paraCount].maxVisibleCharacters = 0;

            dayText = day[DaySystem.instance.dayCount].ParaTexts[paraCount];

            float _speed = 1f - textAnimationSpeed;

            int totalCharacters = day[DaySystem.instance.dayCount].ParaTexts[paraCount].textInfo.characterCount;

            for (int i = 0; i <= totalCharacters; i++)
            {
                day[DaySystem.instance.dayCount].ParaTexts[paraCount].maxVisibleCharacters = i;

                if (i == totalCharacters)
                {
                    typing = false;

                    paraCount++;

                    //textAudio.Stop();
                }

                yield return new WaitForSeconds(0.1f * _speed);
            }
        }

        public void StartDayTextCoroutine(DayTexts[] day)
        {
            if (paraCount >= day[DaySystem.instance.dayCount].ParaTexts.Length)
            {
                Debug.Log("No more paratexts");
                //Display Gold Recap
                if (!DaySystem.instance.displayingGold)
                {
                    DaySystem.instance.GoldRecap();
                    paraCount = 0;
                }

            }
            else
            {
                StartCoroutine(WriteTextToTextmeshDay(day));
            }
        }

        public void CompleteDayText()
        {
            if (typing)
            {
                Debug.Log("complete text");
                StopAllCoroutines();
                typing = false;
                //dayText.text = currentMessage;
                dayText.maxVisibleCharacters = dayText.textInfo.characterCount;
                paraCount++;
                //textAudio.Stop();
            }
            else
            {
                DaySystem.instance.EndOfDayScroll();
            }
        }
    }
}