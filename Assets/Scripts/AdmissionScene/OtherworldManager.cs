using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtherworldManager : MonoBehaviour
{
    private static int MaxDays = 10;

    [SerializeField]
    private OtherworlderQueue otherworlderQueue;

    [SerializeField]
    private Animator contractAnimator;
    [SerializeField]
    private TextMeshProUGUI contractName;

    [SerializeField]
    private Image portraitBody;
    [SerializeField]
    private Image portraitHair;
    [SerializeField]
    private Image portraitTopClothing;

    [SerializeField]
    private TextMeshProUGUI contractText;

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
            contractAnimator.SetTrigger("Hide");
            return;
        }

        if(otherworlderQueue.GetRemainingOtherworlders() == 0) {
            EndDay();
        }

        if(otherworlderQueue.IsProgressing()) {
            if(preparedContract) {
                contractAnimator.SetTrigger("Hide");
                preparedContract = false;
            }
        }
        else {
            if(!preparedContract)
            {
                Globals global = Globals.Instance;
                HumanData data = otherworlderQueue.GetNextHumanData();
                contractName.text = data.firstName + " " + data.surname;
                contractAnimator.SetTrigger("Show");

                portraitBody.color = data.skinColor;
                portraitHair.color = data.hairColor;
                portraitTopClothing.color = data.topClothingColor;

                string memoryText = "";
                memoryText += "When I was a child, " + global.ChildhoodMemories[data.childhoodMemory].text;
                if(data.age > AgeGroup.CHILD) {
                    memoryText += " At my adulthood, " + global.AdulthoodMemories[data.adulthoodMemory].text;
                }
                if(data.age > AgeGroup.ADULT) {
                    memoryText += " After becoming an old man, " + global.GeezerhoodMemories[data.adulthoodMemory].text;
                }
                contractText.text = memoryText;

                preparedContract = true;
            }
        }
    }

    private void PrepareNewDay() {
        int otherworlderCount = (int)(10 + 5 * Random.value + Random.value * currentDay / 2);
        otherworlderQueue.PrepareQueue(otherworlderCount, currentKarma);

        currentKarma += (int)(Mathf.Sign(currentKarma) * ((Random.value > 0.2 ? 1 : 0) * 2))
                    + (int)(2 * Random.value);
        startedDay = true;
    }

    private void EndDay() {
        currentDay++;
        if (currentKarma == -10 || currentKarma == 10) {
            Debug.Log("you lose lol");
        }
        else if(currentDay > MaxDays) {
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

    public void OnSaveClick() {
        Debug.Log("UNITY SAVES GAMING (NOT)"); 
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
