using System;
using System.Collections.ObjectModel;
using UnityEngine;

public class Globals : MonoBehaviour
{
    private static Globals _instance = null;

    private StringList _names;
    private StringList _surnames;
    private StringList _childhoodMemories;
    private StringList _adulthoodMemories;
    private StringList _geezerhoodMemories;
    private Sprite[] _hairs;
    private Sprite[] _topClothings;
    private Sprite[] _bottomClothings;

    public ReadOnlyCollection<string> Names {
        get { return _names.values; }
    }
    public ReadOnlyCollection<string> Surnames
    {
        get { return _surnames.values; }
    }
    public ReadOnlyCollection<Sprite> Hairs {
        get {
            return Array.AsReadOnly(_hairs);
        }
    }
    public ReadOnlyCollection<Sprite> TopClothings {
        get {
            return Array.AsReadOnly(_topClothings);
        }
    }
    public ReadOnlyCollection<Sprite> BottomClothings {
        get {
            return Array.AsReadOnly(_bottomClothings);
        }
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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_instance != this) {
            Destroy(this);
        }
    }

    private void OnDestroy() {
        if (_instance == this) _instance = null;
    }
}
