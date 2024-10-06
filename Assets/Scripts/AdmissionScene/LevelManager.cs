using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Animator transitionAnimator;

    [SerializeField]
    private GameObject allChildren;

    [SerializeField]
    private StringList levelList;

    [SerializeField]
    private int trainingLevelCount = 0;

    [SerializeField]
    private OtherworldManager previousSceneParent;

    private HumanData humanData;

    int currentTrainingStage = 0;

    private IEnumerator GenericCoroutine(float waitTime, Action action) {
        yield return new WaitForSeconds(waitTime);

        action();
    }

    public HumanData GetLevelHuman() {
        return humanData;
    }

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    public void GoToMinigame(HumanData data) {
        allChildren.SetActive(true);

        string targetLevel;
        if (currentTrainingStage < trainingLevelCount) {
            targetLevel = levelList.values[currentTrainingStage];
            currentTrainingStage++;
        }
        else {
            int levelCount = levelList.values.Count - trainingLevelCount;

            targetLevel = levelList.values[Random.Range(trainingLevelCount, levelList.values.Count)];
        }

        transitionAnimator.SetTrigger("DoTransition");
        StartCoroutine(GenericCoroutine(1f, delegate() {
            DontDestroyOnLoad(previousSceneParent.gameObject);
            previousSceneParent.gameObject.SetActive(false);
            SceneManager.LoadSceneAsync(targetLevel).completed += delegate (AsyncOperation op) {
                allChildren.gameObject.SetActive(false);
            };
        }));
    }

    public void ReturnFromMinigame(GameObject minigameSceneParent, bool minigameWon) {
        allChildren.SetActive(true);

        minigameSceneParent.gameObject.SetActive(false);
        Destroy(minigameSceneParent);
        
        _camera.gameObject.SetActive(true);
        transitionAnimator.Play("EndTransition");

        SceneManager.MoveGameObjectToScene(previousSceneParent.gameObject, SceneManager.GetActiveScene());
        previousSceneParent.gameObject.SetActive(true);
        previousSceneParent.MinigameFinished(minigameWon);
        allChildren.SetActive(false);
    }
}
