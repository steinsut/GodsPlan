using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class TypewritingText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioResource audioResource;

    private int currentLetters = 0;
    private float timeElapsed = 0;
    private bool typing = false;

    [SerializeField]
    private float textSpeed = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        textMesh.maxVisibleCharacters = 0;

        audioSource.resource = audioResource;
    }

    public void StartTyping() {
        typing = true;
    }

    public void StopTyping() {
        typing = false;
    }

    public void SetText(string text) {
        textMesh.text = text;
        timeElapsed = 0;
        currentLetters = 0;
    }

    // Update is called once per frame
    void Update() {
        if (!typing) return;

        timeElapsed += Time.deltaTime;
        while (timeElapsed >= (1/textSpeed) && currentLetters < textMesh.text.Length) {
            bool playedAudio = false;

            timeElapsed -= 1 / textSpeed;
            currentLetters++;
            textMesh.maxVisibleCharacters = currentLetters;

            if (!playedAudio) { 
                audioSource.Play(); 
                playedAudio = true;
            }
        }
    }
}
