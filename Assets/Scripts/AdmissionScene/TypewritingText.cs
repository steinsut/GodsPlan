using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

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

    private bool stop = false;
    
    [SerializeField]
    private float textSpeed = 1f;

    private IEnumerator Unstop() {
        yield return new WaitForSeconds(0.37f);

        stop = false;
    }

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

    public bool IsCompleted() {
        return currentLetters == textMesh.text.Length;
    }

    public void SetText(string text) {
        textMesh.text = text;
        timeElapsed = 0;
        currentLetters = 0;
    }

    // Update is called once per frame
    void Update() {
        if (!typing) return;
        if (stop) return;

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

            if (textMesh.text[currentLetters - 1] == '.')
            {
                stop = true;
                StartCoroutine(Unstop());
                break;
            }
        }
    }
}
