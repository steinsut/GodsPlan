using UnityEngine;

public class KarmaScale : MonoBehaviour
{
    [SerializeField]
    private float maxRotation = 20f;

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
        startRotation = scaleRod.transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, (newKarma / 3) * (maxRotation / -2));

        tipping = true;
    }

    public void Update() {
        if (tipping) {
            scaleRod.transform.rotation =
                Quaternion.RotateTowards(scaleRod.transform.rotation,
                                        targetRotation,
                                        Time.deltaTime * rotateSpeed);

            if (scaleRod.transform.rotation == startRotation) { 
                tipping = false;
            }
        }
        badArm.position = leftPlaceholder.position;
        goodArm.position = rightPlaceholder.position;
    }
}
