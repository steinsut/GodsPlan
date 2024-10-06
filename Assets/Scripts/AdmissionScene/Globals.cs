using System;
using System.Collections.ObjectModel;
using UnityEngine;

public class Globals : MonoBehaviour
{
    private static Globals _instance = null;

    private StringList _surnames;
    private MemoryList _childhoodMemories;
    private MemoryList _adulthoodMemories;
    private MemoryList _geezerhoodMemories;
    private HumanResources _femaleSprites;
    private HumanResources _maleSprites;

    public ReadOnlyCollection<string> Surnames {
        get { return _surnames.values; }
    }
    public HumanResources  FemaleSprites {
        get { return _femaleSprites; }
    }
    public HumanResources MaleSprites {
        get { return _maleSprites; }
    }
    public ReadOnlyCollection<Memory> ChildhoodMemories {
        get { return _childhoodMemories.values; }
    }
    public ReadOnlyCollection<Memory> AdulthoodMemories {
        get { return _adulthoodMemories.values; }
    }
    public ReadOnlyCollection<Memory> GeezerhoodMemories
    {
        get { return _geezerhoodMemories.values; }
    }

    public static Globals Instance {  
        get { 
            if(_instance == null) {
                _instance = (new GameObject()).AddComponent<Globals>();
            }
            return _instance;
        } 
    }

    private void Awake() {
        _surnames = Resources.Load<StringList>("Default/Last Names");

        _femaleSprites = Resources.Load<HumanResources>("Default/Female Sprites");
        _maleSprites = Resources.Load<HumanResources>("Default/Hate Speech");

        _childhoodMemories = Resources.Load<MemoryList>("Default/Childhood Memories");
        _adulthoodMemories = Resources.Load<MemoryList>("Default/Adulthood Memories");
        _geezerhoodMemories = Resources.Load<MemoryList>("Default/Geezerhood Memories");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy() {
        if (_instance == this) _instance = null;
    }
}
