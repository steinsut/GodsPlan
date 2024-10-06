using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OtherworldManager : MonoBehaviour
{
    [Serializable]
    public class State {
        public int day;
        public int karma;
        public HumanData[] humanData;
        public Quaternion scaleRotation;
        public float musicTime;
    }

    [Header("Objects")]
    [SerializeField]
    private OtherworlderQueue otherworlderQueue;
    [SerializeField]
    private KarmaScale karmaScale;

    [SerializeField]
    private Animator contractAnimator;
    [SerializeField]
    private TextMeshProUGUI contractName;

    [SerializeField]
    private Image portraitHair;
    [SerializeField]
    private Image portraitHead;
    [SerializeField]
    private Image portraitBody;

    [SerializeField]
    private Sprite portraitBodyChild;
    [SerializeField]
    private Sprite portraitBodyAdult;

    [SerializeField]
    private TypewritingText contractText;

    [SerializeField]
    private TextMeshProUGUI dayText;

    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button denyButton;

    [Header("Game Values")]
    [SerializeField]
    private int maxDays = 10;

    [SerializeField]
    private AudioClip otherworldMusic;

    [SerializeField]
    private int minimumPeoplePerDay = 10;

    private int currentDay = 1;
    private int currentKarma = 0;

    private bool startedDay = false;
    private bool preparedContract = false;

    private void Start() {
        if (!startedDay) { 
            PrepareNewDay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!startedDay) {
            HideContract();
            return;
        }

        if(otherworlderQueue.GetRemainingOtherworlders() == 0) {
            EndDay();
            return;
        }

        if (Input.GetButtonDown("accept"))
        {
            OnSaveClick();
        } else if (Input.GetButtonDown("deny"))
        {
            OnDenyClick();
        }

        if(otherworlderQueue.IsProgressing()) {
            HideContract();
        }
        else {
            if(!preparedContract)
            {
                saveButton.interactable = true;
                denyButton.interactable = true;

                Globals globals = Globals.Instance;
                HumanData data = otherworlderQueue.GetNextHumanData();
                contractName.text = data.firstName + " " + data.surname;

                HumanResources sprites;
                if (data.sex == Sex.FEMALE) {
                    sprites = globals.FemaleSprites;
                }
                else {
                    sprites = globals.MaleSprites;
                }
                HumanResources.Collection collection = sprites.GetAppropriateCollection(data.age);
                portraitHair.enabled = true;
                portraitHair.sprite = collection.PortraitHairs[data.hairId];
                portraitHead.sprite = collection.PortraitHeads[data.headId];
                portraitHair.color = data.hairColor;
                portraitHead.color = data.skinColor;
                if (portraitHair.sprite == null) { 
                    portraitHair.enabled = false;
                }
                if(data.age == AgeGroup.CHILD) {
                    portraitBody.sprite = portraitBodyChild;
                }
                else {
                    portraitBody.sprite = portraitBodyAdult;
                }


                string memoryText = "";
                memoryText += "When I was a child, " + globals.ChildhoodMemories[data.childhoodMemory].text;
                if(data.age > AgeGroup.CHILD) {
                    memoryText += " At my adulthood, " + globals.AdulthoodMemories[data.adulthoodMemory].text;
                }
                if(data.age > AgeGroup.ADULT) {
                    memoryText += " After becoming an old man, " + globals.GeezerhoodMemories[data.geezerhoodMemory].text;
                }
                contractText.SetText(memoryText);
                contractText.StartTyping();

                preparedContract = true;
                contractAnimator.SetTrigger("Show");
            }
        }
    }

    private void HideContract() {
        if (preparedContract) { 
            contractAnimator.SetTrigger("Hide");
            contractText.StopTyping();
            preparedContract = false;
        }
    }

    private void PrepareNewDay() {
        int otherworlderCount = (int)(minimumPeoplePerDay + 5 * Random.value + Random.value * currentDay / 2);
        otherworlderQueue.PrepareQueue(otherworlderCount, currentKarma);

        currentKarma += (int)(Mathf.Sign(currentKarma) * ((Random.value > 0.2 ? 1 : 0) * 2))
                    + (int)(Math.Sign(currentKarma) * 2 * Random.value);
        karmaScale.Tip(currentKarma);

        dayText.text = "Day " + currentDay;
        
        startedDay = true;
    }

    private void EndDay() {
        currentDay++;
        startedDay = false;
        if (currentKarma == -10 || currentKarma == 10) {
            Debug.Log("you lose lol");
        }
        else if(currentDay > maxDays) {
            if (currentKarma >= -2 || currentKarma <= 2) {
                Debug.Log("you win lol");
            }
            else {
                Debug.Log("you lose lol");
            }
        }
        else {
            PrepareNewDay();
        }
    }

    public void MinigameFinished(bool succeeded, State state) {
        preparedContract = true;
        startedDay = true;

        currentDay = state.day;
        currentKarma = state.karma;
        otherworlderQueue.SetHumanData(state.humanData);
        karmaScale.transform.rotation = state.scaleRotation;
        LevelManager.Instance.GetMusicPlayer().SetAudio(otherworldMusic);
        LevelManager.Instance.GetMusicPlayer().Seek(state.musicTime);
        LevelManager.Instance.GetMusicPlayer().FadeIn(2.4f);

        dayText.text = "Day " + currentDay;

        if (succeeded) {
            Debug.Log("Current Karma: " + currentKarma);
            Debug.Log("Gained Karma: " + otherworlderQueue.GetNextHumanData().GetTotalKarma());
            currentKarma += otherworlderQueue.GetNextHumanData().GetTotalKarma();
            currentKarma = Math.Clamp(currentKarma, -10, 10);
            karmaScale.Tip(currentKarma);
        }
        otherworlderQueue.ProgressQueue(succeeded);
    }

    public void OnSaveClick() {
        contractText.StopTyping();
        State state = new State();
        state.day = currentDay;
        state.karma = currentKarma;
        state.humanData = otherworlderQueue.GetAllHumanData();
        state.scaleRotation = karmaScale.transform.rotation;
        state.musicTime = LevelManager.Instance.GetMusicPlayer().GetTime();
        LevelManager.Instance.GoToMinigame(otherworlderQueue.GetNextHumanData(), state);
        saveButton.interactable = false;
        denyButton.interactable = false;
    }

    public void OnDenyClick() {
        otherworlderQueue.ProgressQueue(false);
        saveButton.interactable = false;
        denyButton.interactable = false;
    }
}
