using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private TypewritingText dialogue;

    string[] sentences = {
        "As God said, your job is to choose individuals so that God’s plan can come into fruition.",
        "You must balance goodness and badness in the world.",
        "If goodness or badness goes over a threshold Earth is destroyed.",
        "The person who comes here will tell their most important stories.",
        "You can then decide to save them according to your needs.",
        "But to successfully save someone, you must physically save them.",
        "Congratulations you successfully completed your training. Now your first day starts."
    };

    int currentDialogue = 0;

    void Start() {
        dialogue.SetText(sentences[currentDialogue]);
        dialogue.StartTyping();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(dialogue.IsCompleted() && Input.GetButtonDown("accept")) {
            if (currentDialogue < sentences.Length) {
                currentDialogue++;
                dialogue.SetText(sentences[currentDialogue]);
                dialogue.StartTyping();
            }
            else {
                SceneManager.LoadSceneAsync("AdmissionScene");
            }
        }
    }
}
