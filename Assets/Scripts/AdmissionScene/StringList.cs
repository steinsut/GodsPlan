using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "StringList", menuName = "Scriptable Objects/StringList")]
public class StringList : ScriptableObject {

    [SerializeField]
    private List<string> list = new List<string>();

    public ReadOnlyCollection<string> values {
        get {
            return list.AsReadOnly();
        }
    }
}
