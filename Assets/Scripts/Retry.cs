using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] texts;
    private KeyCode key;

    bool pressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && !pressed) {
            for (int i = 0; i < texts.Length; i++){
                texts[i].gameObject.SetActive(false);
            }
            LevelManager.Instance.RestartGame();
            pressed = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
