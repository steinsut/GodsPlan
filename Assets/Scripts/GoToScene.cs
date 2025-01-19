using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text1;
    [SerializeField]
    private TextMeshProUGUI text2;
    [SerializeField]
    private KeyCode key;
    [SerializeField]
    private string sceneName;

    bool pressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        if (Input.GetKeyDown(key) && !pressed) {
            text1.gameObject.SetActive(false);
            text2.gameObject.SetActive(false);
            SceneManager.LoadSceneAsync(sceneName);
            pressed = true;
        }
    }
}
