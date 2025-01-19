using System;
using System.Collections;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance = null;

    public static LevelManager Instance
    {
        get {
            return _instance;
        }
    }

    private Camera otherworldCamera;

    private OtherworldManager otherworldManager;

    [SerializeField]
    private Animator transitionAnimator;

    [SerializeField]
    private AudioClip gameMusic;


    [SerializeField]
    private MusicPlayer musicPlayer;

    [SerializeField]
    private StringList levelList;

    [SerializeField]
    private GameObject allChildren;

    [SerializeField]
    private int trainingLevelCount = 0;

    private HumanData humanData;
    private OtherworldManager.State otherworldState;

    int currentTrainingStage = 0;

    private IEnumerator GenericCoroutine(float waitTime, Action action) {
        yield return new WaitForSeconds(waitTime);

        action();
    }

    public HumanData GetLevelHuman() {
        return humanData;
    }

    public MusicPlayer GetMusicPlayer() {
        return musicPlayer;
    }

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this);
            otherworldCamera = Camera.allCameras[0];
            otherworldManager = GameObject.Find("Otherworld Manager").GetComponent<OtherworldManager>();
        }
        else { 
            Destroy(gameObject);
        }
    }

    public void GoToMinigame(HumanData data, OtherworldManager.State state) {
        humanData = data;
        otherworldState = state;
        allChildren.SetActive(true);

        string targetLevel;
        if (currentTrainingStage < trainingLevelCount) {
            targetLevel = levelList.values[currentTrainingStage];
            currentTrainingStage++;
        }
        else {
            int levelCount = levelList.values.Count - trainingLevelCount;

            targetLevel = levelList.values[Random.Range(0, levelList.values.Count)];
        }

        transitionAnimator.SetTrigger("DoTransition");
        musicPlayer.FadeOut(0.75f);
        StartCoroutine(GenericCoroutine(1.5f, delegate() {
            SceneManager.LoadSceneAsync(targetLevel).completed += delegate (AsyncOperation op) {
                allChildren.gameObject.SetActive(false);
                musicPlayer.ResetVolume();
                musicPlayer.SetAudio(null);
            };
        }));
    }

    public void ReturnFromMinigame(GameObject minigameSceneParent, bool minigameWon) {
        allChildren.SetActive(true);
        SceneManager.LoadSceneAsync("AdmissionScene").completed += delegate(AsyncOperation op) {
            transitionAnimator.Play("EndTransition");

            StartCoroutine(GenericCoroutine(0.5f, delegate () {
                allChildren.SetActive(false);
            }));

            otherworldCamera = Camera.allCameras[0];
            otherworldManager = GameObject.Find("Otherworld Manager").GetComponent<OtherworldManager>();
            otherworldManager.MinigameFinished(minigameWon, otherworldState);
        };
    }

    public void GoToLose()
    {
        allChildren.SetActive(true);
        transitionAnimator.SetTrigger("DoTransition");
        musicPlayer.FadeOut(0.75f);
        StartCoroutine(GenericCoroutine(1.5f, delegate () {
            SceneManager.LoadSceneAsync("Lose").completed += delegate (AsyncOperation op) {
                allChildren.gameObject.SetActive(false);
                musicPlayer.ResetVolume();
                musicPlayer.SetAudio(null);
            };
        }));
    }

    public void GoToWin()
    {
        allChildren.SetActive(true);
        transitionAnimator.SetTrigger("DoTransition");
        musicPlayer.FadeOut(0.75f);
        StartCoroutine(GenericCoroutine(1.5f, delegate () {
            SceneManager.LoadSceneAsync("Win").completed += delegate (AsyncOperation op) {
                allChildren.gameObject.SetActive(false);
                musicPlayer.ResetVolume();
                musicPlayer.SetAudio(null);
            };
        }));
    }

    public void RestartGame()
    {
        otherworldState = new OtherworldManager.State();
        ReturnFromMinigame(null, false);
    }
}
