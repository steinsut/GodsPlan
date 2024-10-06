using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class HumanData {
    public static HumanData CreateRandom() {
        Globals globals = Globals.Instance;
        HumanData human = new HumanData();

        human.firstName = globals.Names[Random.Range(0, globals.Names.Count)];
        human.surname = globals.Surnames[Random.Range(0, globals.Surnames.Count)];
        human.age = (AgeGroup)Random.Range(0, 3);
        human.sex = (Sex)Random.Range(0, 2);

        human.hairColor = Random.ColorHSV(0f, 1f, 0f, 1f);

        HumanSprites sprites;
        if(human.sex == Sex.FEMALE) {
            sprites = globals.FemaleSprites;
        }
        else {
            sprites = globals.MaleSprites;
        }

        HumanSprites.Collection collection = sprites.GetAppropriateCollection(human.age);

        human.hairId = Random.Range(0, collection.PortraitHairs.Count);
        human.headId = Random.Range(0, collection.PortraitHeads.Count);
        
        //human.topClothingId = Random.Range(0, Globals.Instance.TopClothings.Count);
        human.topClothingColor = Random.ColorHSV();
        //human.bottomClothingId = Random.Range(0, Globals.Instance.BottomClothings.Count);
        human.bottomClothingColor = Random.ColorHSV();

        human.childhoodMemory = Random.Range(0, globals.ChildhoodMemories.Count);
        human.adulthoodMemory = Random.Range(0, globals.AdulthoodMemories.Count);
        human.geezerhoodMemory = Random.Range(0, globals.GeezerhoodMemories.Count);

        return human;
    }

    //Personal Information
    public string firstName = "";
    public string surname = "";
    public AgeGroup age = AgeGroup.CHILD;
    public Sex sex = Sex.FEMALE;

    //Looks
    public int hairId = -1;
    public Color hairColor = Color.white;

    public int headId = -1;

    public int topClothingId = -1;
    public Color topClothingColor = Color.white;

    public int bottomClothingId = -1;
    public Color bottomClothingColor = Color.white;

    //Memories
    public int childhoodMemory = -1;
    public int adulthoodMemory = -1;
    public int geezerhoodMemory = -1;

    public int GetTotalKarma() {
        Globals globals = Globals.Instance;

        int totalKarma = globals.ChildhoodMemories[childhoodMemory].karma;
        if (age > AgeGroup.CHILD)
        {
            totalKarma += globals.AdulthoodMemories[adulthoodMemory].karma;
        }
        if (age > AgeGroup.ADULT)
        {
            totalKarma += globals.GeezerhoodMemories[geezerhoodMemory].karma;
        }

        return totalKarma;
    }
}
