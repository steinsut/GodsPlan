using System.Collections;
using UnityEngine;

public class endTransStateManager : MonoBehaviour
{
    [SerializeField]
    private Animation insert;

    [SerializeField]
    private GameObject sceneParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        insert.Rewind();
        insert.Play("cloudsPass");
    }

    void fadeOut()
    {
        insert.Play("cloudCome");
        fading = true;
    }
    bool? succesState = null;
    public void setupReturn(bool success)
    {
        succesState = success;
        exited = true;
    }

    void exitScene()
    {
        if (succesState == null) {
            succesState = false;
        }
        LevelManager.Instance.ReturnFromMinigame(sceneParent, (bool)succesState);
    }
    bool exited = false;
    bool fading = false;

    // Update is called once per frame
    void Update()
    {
        if (succesState == null)
        {
            return;
        } else
        {
            if (!insert.isPlaying && exited && !fading)
            {
                fadeOut();
            } else if (!insert.isPlaying && fading)
            {
                exitScene();
            }
        }
    }
}
