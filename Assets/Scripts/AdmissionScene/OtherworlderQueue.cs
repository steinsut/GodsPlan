using System.Collections.Generic;
using UnityEngine;

public class OtherworlderQueue : MonoBehaviour
{
    [SerializeField]
    private int otherworlderCount;

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
    private List<HumanData> humanData = new List<HumanData>();

    private bool progressing = false;
    private bool savedFront = false;
    private bool frontDisappeared = false;

    HumanData[] GenerateHumans(int count) {
        HumanData[] humans = new HumanData[count];

        for(int i = 0; i < count; i++) {
            humans[i] = HumanData.CreateRandom();
        }

        return humans;
    }

    public void ProgressQueue(bool saveFront) {
        progressing = true;
        savedFront = saveFront;
    }

    public bool IsProgressing() {
        return progressing;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        humanData.AddRange(GenerateHumans(otherworlderCount));
        for (int i = 0; i < otherworlderCount; i++) {
            otherworlders.Add(Instantiate<Otherworlder>(otherworlderPrefab));
            otherworlders[i].SetHumanData(humanData[i]);
            otherworlders[i].transform.position = new Vector3(
                doorstop.position.x - i * queueSpacing,
                otherworlders[i].transform.position.y,
                otherworlders[i].transform.position.z);
        }
    }

    private void Start() {
        ProgressQueue(true);
    }

    // Update is called once per frame
    void Update() {
        if (progressing) {
            if (!savedFront) {
                if (otherworlders[0].transform.position.x < exit.position.x) {
                    otherworlders[0].transform.Translate(queueLeaveSpeed * Time.deltaTime, 0f, 0f);
                    for (int i = 1; i < otherworlders.Count; i++) {
                        float queueMovement = queueMoveSpeed * 0.75f * Time.deltaTime;
                        otherworlders[i].transform.Translate(queueMovement, 0f, 0f);
                    }
                    if (otherworlders[1].transform.position.x > doorstop.position.x)
                    {
                        for (int i = otherworlderCount - 1; i >= 1; i--)
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
                    Otherworlder otherworlder = otherworlders[0];
                    otherworlders.RemoveAt(0);
                    otherworlders.Add(otherworlder);
                    humanData.RemoveAt(0);
                    humanData.Add(HumanData.CreateRandom());
                    otherworlder.SetHumanData(humanData[humanData.Count - 1]);
                    otherworlder.transform.position = new Vector3(
                        otherworlders[otherworlderCount - 2].transform.position.x - queueSpacing,
                        otherworlder.transform.position.y,
                        otherworlder.transform.position.z);

                    frontDisappeared = true;
                }
                if (otherworlders[0].transform.position.x < doorstop.position.x) {
                    for(int i = 0; i < otherworlderCount; i++) {
                        float movement = queueMoveSpeed * Time.deltaTime;
                        otherworlders[i].transform.Translate(movement, 0f, 0f);
                    }
                    if(otherworlders[0].transform.position.x
                        > doorstop.position.x) {
                        for (int i = otherworlderCount - 1; i >= 0; i--) {
                            float overstep = doorstop.position.x -
                                (otherworlders[0].transform.position.x);
                            otherworlders[i].transform.Translate(overstep, 0f, 0f);
                        }
                    }
                }
                else {
                    progressing = false;
                    savedFront = false;
                    frontDisappeared = false;
                }
            }
        }
    }
}
