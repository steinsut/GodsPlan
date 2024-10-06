using Unity.Cinemachine;
using UnityEngine;

public class CineInit : MonoBehaviour
{
    [SerializeField]
    CinemachineCamera cam;

    int counter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.UpdateCameraState(Vector3.zero, 1f);
    }

    // Update is called once per frame
    void Update()
    {
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
