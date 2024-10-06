using System;
using System.Collections.ObjectModel;
using UnityEngine;

public class Globals : MonoBehaviour
{
    private static Globals _instance = null;

    private StringList _names;
    private StringList _surnames;
    private MemoryList _childhoodMemories;
    private MemoryList _adulthoodMemories;
    private MemoryList _geezerhoodMemories;
    private HumanSprites _femaleSprites;
    private HumanSprites _maleSprites;

    public ReadOnlyCollection<string> Names {
        get { return _names.values; }
    }
    public ReadOnlyCollection<string> Surnames {
        get { return _surnames.values; }
    }
    public HumanSprites  FemaleSprites {
        get { return _femaleSprites; }
    }
    public HumanSprites MaleSprites {
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
        _names = Resources.Load<StringList>("Default/First Names");
        _surnames = Resources.Load<StringList>("Default/Last Names");

        _femaleSprites = Resources.Load<HumanSprites>("Default/Female Sprites");
        _maleSprites = Resources.Load<HumanSprites>("Default/Hate Speech");

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
