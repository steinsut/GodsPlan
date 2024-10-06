using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OtherworldManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private LevelManager levelManager;
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

    [Header("Game Values")]
    [SerializeField]
    private int maxDays = 10;

    [SerializeField]
    private int minimumPeoplePerDay = 10;

    private int currentDay = 1;
    private int currentKarma = 0;

    private bool startedDay = false;
    private bool preparedContract = false;

    private void Start() {
        PrepareNewDay();
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

        if(otherworlderQueue.IsProgressing()) {
            if(preparedContract) {
                HideContract();
                preparedContract = false;
            }
        }
        else {
            if(!preparedContract)
            {
                Globals globals = Globals.Instance;
                HumanData data = otherworlderQueue.GetNextHumanData();
                contractName.text = data.firstName + " " + data.surname;

                HumanSprites sprites;
                if (data.sex == Sex.FEMALE) {
                    sprites = globals.FemaleSprites;
                }
                else {
                    sprites = globals.MaleSprites;
                }
                HumanSprites.Collection collection = sprites.GetAppropriateCollection(data.age);
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
                    memoryText += " After becoming an old man, " + globals.GeezerhoodMemories[data.adulthoodMemory].text;
                }
                contractText.SetText(memoryText);
                contractText.StartTyping();

                contractAnimator.SetTrigger("Show");

                preparedContract = true;
            }
        }
    }

    private void HideContract() {
        contractAnimator.SetTrigger("Hide");
        contractText.StopTyping();
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

    public void MinigameFinished(bool succeeded) {
        if (succeeded) {
            currentKarma += otherworlderQueue.GetNextHumanData().GetTotalKarma();
            currentKarma = Math.Clamp(currentKarma, -10, 10);
            karmaScale.Tip(currentKarma);
        }
        otherworlderQueue.ProgressQueue(succeeded);
    }

    public void OnSaveClick() {
        levelManager.GoToMinigame(otherworlderQueue.GetNextHumanData());
    }

    public void OnDenyClick() {
        if(otherworlderQueue.GetRemainingOtherworlders() == 1) {
            otherworlderQueue.ProgressQueue(false);
        } 
        else {
            otherworlderQueue.ProgressQueue(false);
        }
    }
}
