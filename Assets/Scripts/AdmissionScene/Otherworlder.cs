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
    [SerializeField]
    private SpriteRenderer headSprite;

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

        HumanResources sprites = data.sex == Sex.FEMALE ? Globals.Instance.FemaleSprites 
            : Globals.Instance.MaleSprites;

        HumanResources.Collection collection = sprites.GetAppropriateCollection(data.age);

        headSprite.sprite = collection.GhostHeads[0];
        headSprite.color = data.skinColor;

        hairSprite.sprite = collection.GhostHairs[data.hairId];
        hairSprite.color = data.hairColor;
    }

    public void TurnToPlayer() {

    }
}
