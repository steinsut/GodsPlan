using System.Collections;
using UnityEngine;

public class endTransStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject childrenObject;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float waitTime;
    
    private IEnumerator EndTransitionCoroutine(bool success) {
        yield return new WaitForSeconds(waitTime);

        LevelManager instance = null;
        //instance = LevelManager.Instance;
        
        instance.ReturnFromMinigame(gameObject, success);
    }

    private IEnumerator StartTransitionCoroutine() {

        childrenObject.SetActive(true);
        animator.Play("EndTransition");

        yield return new WaitForSeconds(waitTime);

        childrenObject.SetActive(false);
    }

    void EndMinigameTransition(bool success) {
        childrenObject.SetActive(success);
        animator.Play("DoTransiton");

        StartCoroutine(EndTransitionCoroutine(success));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
