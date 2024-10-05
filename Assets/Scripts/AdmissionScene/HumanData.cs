using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class HumanData {
    public static HumanData CreateRandom() {
        ReadOnlyCollection<string> names = Globals.Instance.Names;
        ReadOnlyCollection<string> surnames = Globals.Instance.Surnames;
        HumanData human = new HumanData();

        human.firstName = names[Random.Range(0, names.Count)];
        human.surname = surnames[Random.Range(0, surnames.Count)];
        human.age = (AgeGroup)Random.Range(0, 3);
        human.skinColor = Random.ColorHSV(0f, 1f, 0f, 1f);

        //human.hairId = Random.Range(0, Globals.Instance.Hairs.Count);
        human.hairColor = Random.ColorHSV();
        //human.topClothingId = Random.Range(0, Globals.Instance.TopClothings.Count);
        human.topClothingColor = Random.ColorHSV();
        //human.bottomClothingId = Random.Range(0, Globals.Instance.BottomClothings.Count);
        human.bottomClothingColor = Random.ColorHSV();

        //human.childhoodMemory = Random.Range(0, Globals.Instance.ChildhoodMemories.values.Count);
        //human.adulthoodMemory = Random.Range(0, Globals.Instance.AdulthoodMemories.values.Count);
        //human.geezerhoodMemory = Random.Range(0, Globals.Instance.GeezerhoodMemories.values.Count);

        return human;
    }

    //Personal Information
    public string firstName = "";
    public string surname = "";
    public AgeGroup age = AgeGroup.NONE;

    //Looks
    public Color skinColor = Color.white;
    
    public int hairId = -1;
    public Color hairColor = Color.white;

    public int topClothingId = -1;
    public Color topClothingColor = Color.white;

    public int bottomClothingId = -1;
    public Color bottomClothingColor = Color.white;


    //Memories
    public int childhoodMemory = -1;
    public int adulthoodMemory = -1;
    public int geezerhoodMemory = -1;
}
