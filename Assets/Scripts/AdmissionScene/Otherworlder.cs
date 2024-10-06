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
    private SpriteRenderer hairSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        startingY = transform.localPosition.y;
        offset = Random.value;
        flySpeed = Random.value * 0.64f;
        flyHeight = 0.083f + Random.value * 0.0656f;
    }

    // Update is called once per frame
    void Update() {
        float yOffset = Mathf.Sin(Time.time * flySpeed + offset) * flyHeight;

        if (yOffset < 0) {
            yOffset = yOffset * (flyHeight / 1.3f); 
        }
        
        transform.localPosition = new Vector3(transform.localPosition.x, startingY + yOffset, transform.localPosition.z);
    }

    public HumanData GetHumanData() {
        return humanData;
    }

    public void SetHumanData(HumanData data) {
        humanData = data;

        HumanSprites sprites = data.sex == Sex.FEMALE ? Globals.Instance.FemaleSprites 
            : Globals.Instance.MaleSprites;

        HumanSprites.Collection collection = sprites.GetAppropriateCollection(data.age);

        hairSprite.sprite = collection.GhostHeads[humanData.hairId];
        //hairSprite.color = data.hairColor;
    }

    public void TurnToPlayer() {

    }
}
