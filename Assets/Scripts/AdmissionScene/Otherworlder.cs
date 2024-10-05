using UnityEngine;
using Random = UnityEngine.Random;

public class Otherworlder : MonoBehaviour
{
    private float startingY;
    private float offset;
    private float flySpeed;
    private float flyHeight;

    private HumanData humanData;

    [SerializeField]
    private SpriteRenderer bodySprite;

    [SerializeField]
    private SpriteRenderer hairSprite;

    [SerializeField]
    private SpriteRenderer topClothingSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        startingY = transform.localPosition.y;
        offset = Random.value * 4;
        flySpeed = Random.value * 2;
        flyHeight = 0.3f + Random.value * 0.28f;
    }

    // Update is called once per frame
    void Update() {
        float yOffset = Mathf.Sin(Time.time * flySpeed + offset) * flyHeight;

        if (yOffset < 0) {
            yOffset = yOffset * (flyHeight / 2); 
        }
        
        transform.localPosition = new Vector3(transform.localPosition.x, startingY + yOffset, transform.localPosition.z);
    }

    public HumanData GetHumanData() {
        return humanData;
    }

    public void SetHumanData(HumanData data) {
        humanData = data;

        bodySprite.color = data.skinColor;
        hairSprite.color = data.hairColor;
        topClothingSprite.color = data.topClothingColor;
    }

    public void TurnToPlayer() {

    }
}
