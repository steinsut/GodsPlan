using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private bool meowed = false;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            Application.Quit();
        }

        if(Input.GetMouseButtonDown(0)) {
            if (!meowed) { 
                SceneManager.LoadSceneAsync("Tutorial");
            }
            else {
                meowed = false;
            }
        }
    }

    void Meow() {
        meowed = true;
        audioSource.Play();
    }
}
