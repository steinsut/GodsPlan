using Unity.Cinemachine;
using UnityEngine;

public class CineInit : MonoBehaviour
{
    [SerializeField]
    CinemachineConfiner2D confiner;

    float cineTime = 0f;

    int counter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cineTime < 0.02f)
        {
            cineTime += Time.deltaTime;
        } else
        {
            if (!confiner.enabled)
            {
                confiner.enabled = true;
            }
        }

        if (counter == 10)
        {
            Start();
        }
        if (counter < 10)
        {
            counter++;
        }
    }
}
