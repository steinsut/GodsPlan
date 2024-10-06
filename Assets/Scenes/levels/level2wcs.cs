using UnityEngine;

public class level2wcs : MonoBehaviour
{
    [SerializeField]
    ToggleController electricityToggle;
    [SerializeField]
    GameObject levelParent;
    [SerializeField]
    bool invertWinC = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if ((!electricityToggle.toggled && invertWinC) || (electricityToggle.toggled && !invertWinC))
        {
            Debug.Log("WINWINWIN");
            GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().ReturnFromMinigame(levelParent, true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
