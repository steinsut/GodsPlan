using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private StringList levelList;

    [SerializeField]
    private int trainingLevelCount = 0;

    [SerializeField]
    private OtherworldManager previousSceneParent;

    private HumanData humanData;

    int currentTrainingStage = 0;

    public HumanData GetLevelHuman() {
        return humanData;
    }

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    public void GoToMinigame(HumanData data) {
        string targetLevel;
        if (currentTrainingStage < trainingLevelCount) {
            targetLevel = levelList.values[currentTrainingStage];
            currentTrainingStage++;
        }
        else {
            int levelCount = levelList.values.Count - trainingLevelCount;

            targetLevel = levelList.values[Random.Range(trainingLevelCount, levelList.values.Count)];
        }

        //play transition

        DontDestroyOnLoad(previousSceneParent);
        previousSceneParent.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(targetLevel);
    }

    public void ReturnFromMinigame(GameObject minigameSceneParent, bool minigameWon) {
        Destroy(minigameSceneParent);

        SceneManager.MoveGameObjectToScene(previousSceneParent.gameObject, SceneManager.GetActiveScene());
        previousSceneParent.gameObject.SetActive(true);
        previousSceneParent.MinigameFinished(minigameWon);
    }
}
