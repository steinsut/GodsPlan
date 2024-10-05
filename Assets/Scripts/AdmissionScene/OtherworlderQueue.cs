using System.Collections.Generic;
using UnityEngine;

public class OtherworlderQueue : MonoBehaviour
{
    [SerializeField]
    private Transform doorstop;
    [SerializeField]
    private Transform exit;

    [SerializeField]
    private Otherworlder otherworlderPrefab;

    [SerializeField]
    private float queueSpacing = 2.2f;

    [SerializeField]
    private float queueMoveSpeed = 1.1f;

    [SerializeField]
    private float queueLeaveSpeed = 5.5f;

    private List<Otherworlder> otherworlders = new List<Otherworlder>();

    private bool progressing = false;
    private bool savedFront = false;
    private bool frontDisappeared = false;
    private int remainingOtherworlders = 0;

    private HumanData[] GenerateHumans(int count) {
        HumanData[] humans = new HumanData[count];

        for(int i = 0; i < count; i++) {
            humans[i] = HumanData.CreateRandom();
        }

        return humans;
    }

    public int GetRemainingOtherworlders() {
        return remainingOtherworlders;
    }

    public void PrepareQueue(int count, int influenceKarma) {
        for (int i = 0; i < otherworlders.Count; i++) {
            Destroy(otherworlders[i]);
        }
        otherworlders.Clear();

        Globals globals = Globals.Instance;
        List<HumanData> allData = new List<HumanData>();
        HumanData[] generation1 = GenerateHumans(count);
        
        int droppedCount = 0;
        
        for(int i = 0; i < count; i++) {
            HumanData data = generation1[i];
            int totalKarma = globals.ChildhoodMemories[data.childhoodMemory].karma;
            if (data.age > AgeGroup.CHILD) {
                totalKarma += globals.AdulthoodMemories[data.adulthoodMemory].karma;
            }
            if (data.age > AgeGroup.ADULT)
            {
                totalKarma += globals.GeezerhoodMemories[data.geezerhoodMemory].karma;
            }

            if(influenceKarma < 0 && totalKarma > 0 && Random.value >= influenceKarma * 0.05) { allData.Add(data); }
            else if(influenceKarma > 0 && totalKarma < 0 && Random.value >= influenceKarma * 0.05) { allData.Add(data); }
            else if(influenceKarma == 0) { allData.Add(data); }
            else { droppedCount++; }

        }

        allData.AddRange(GenerateHumans(droppedCount));

        for (int i = 0; i < count; i++)
        {
            otherworlders.Add(Instantiate<Otherworlder>(otherworlderPrefab));
            otherworlders[i].SetHumanData(allData[i]);
            otherworlders[i].transform.position = new Vector3(
                doorstop.position.x - i * queueSpacing,
                otherworlders[i].transform.position.y,
                otherworlders[i].transform.position.z);
        }

        remainingOtherworlders = allData.Count;
    }

    public void ProgressQueue(bool saveFront) {
        if(!progressing)        {
            progressing = true;
            savedFront = saveFront;
        }
    }

    public bool IsProgressing() {
        return progressing;
    }

    public HumanData GetNextHumanData() {
        return otherworlders[0].GetHumanData();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (progressing) {
            if (!savedFront) {
                if (otherworlders[0].transform.position.x < exit.position.x) {
                    otherworlders[0].transform.Translate(queueLeaveSpeed * Time.deltaTime, 0f, 0f);
                    for (int i = 1; i < remainingOtherworlders; i++) {
                        float queueMovement = queueMoveSpeed * 0.75f * Time.deltaTime;
                        otherworlders[i].transform.Translate(queueMovement, 0f, 0f);
                    }
                    if (remainingOtherworlders > 1 && otherworlders[1].transform.position.x > doorstop.position.x)
                    {
                        for (int i = remainingOtherworlders - 1; i >= 1; i--)
                        {
                            float overstep = doorstop.position.x - otherworlders[1].transform.position.x;
                            otherworlders[i].transform.Translate(overstep, 0f, 0f);
                        }
                    }
                }
                else {
                    savedFront = true;
                }
            }
            else {
                if(!frontDisappeared) {
                    remainingOtherworlders--;
                    Otherworlder otherworlder = otherworlders[0];
                    Debug.Log(otherworlders.Count);
                    otherworlders.RemoveAt(0);
                    Debug.Log(otherworlders.Count);
                    Debug.Log(otherworlder.GetHumanData().firstName + otherworlder.GetHumanData().surname);
                    Destroy(otherworlder);

                    frontDisappeared = true;
                }
                if (remainingOtherworlders > 0 && otherworlders[0].transform.position.x < doorstop.position.x) {
                    Debug.Log("ji");
                    Debug.Log(remainingOtherworlders);
                    Debug.Log(otherworlders.Count);
                    for(int i = 0; i < remainingOtherworlders; i++) {
                        float movement = queueMoveSpeed * Time.deltaTime;
                        otherworlders[i].transform.Translate(movement, 0f, 0f);
                    }
                    if(otherworlders[0].transform.position.x
                        > doorstop.position.x) {
                        for (int i = remainingOtherworlders - 1; i >= 0; i--) {
                            float overstep = doorstop.position.x -
                                (otherworlders[0].transform.position.x);
                            otherworlders[i].transform.Translate(overstep, 0f, 0f);
                        }
                    }
                }
                else {
                    Debug.Log("wo");
                    progressing = false;
                    savedFront = false;
                    frontDisappeared = false;
                }
            }
        }
    }
}
