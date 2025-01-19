using System;
using UnityEngine;

public class KarmaScale : MonoBehaviour
{
    [SerializeField]
    private float maxRotation = 20f;

    [SerializeField]
    private Transform badWarning;
    [SerializeField]
    private Transform goodWarning;

    [SerializeField]
    private Transform badArm;    
    [SerializeField]
    private Transform goodArm;

    [SerializeField]
    private Transform scaleRod;
    [SerializeField]
    private Transform leftPlaceholder;
    [SerializeField] 
    private Transform rightPlaceholder;

    [SerializeField]
    private float rotateSpeed = 20f;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool tipping = false;

    public void Tip(int newKarma) {
        goodWarning.gameObject.SetActive(false);
        badWarning.gameObject.SetActive(false);
        startRotation = scaleRod.transform.rotation;
        int absKarma = Math.Abs(newKarma);
        int karmaSign = Math.Sign(newKarma);

        if(absKarma <= 2) { targetRotation = Quaternion.Euler(0, 0, 0); }
        else if (absKarma <= 6) { 
            targetRotation = Quaternion.Euler(0, 0, maxRotation / (-2 * karmaSign)); 
        }
        else {
            if (karmaSign < 0) { badWarning.gameObject.SetActive(true); }
            else { goodWarning.gameObject.SetActive(true); }

            targetRotation = Quaternion.Euler(0, 0, maxRotation * (-1 * karmaSign)); 
        }

        tipping = true;
    }

    public void Update() {
        if (tipping) {
            scaleRod.transform.rotation =
                Quaternion.RotateTowards(scaleRod.transform.rotation,
                                        targetRotation,
                                        Time.deltaTime * rotateSpeed);
            
            if (scaleRod.transform.rotation == targetRotation) { 
                tipping = false;
            }
        }
        badArm.position = leftPlaceholder.position;
        goodArm.position = rightPlaceholder.position;
    }
}
