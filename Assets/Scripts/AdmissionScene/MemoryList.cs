using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "MemoryList", menuName = "Scriptable Objects/MemoryList")]
public class MemoryList : ScriptableObject {

    [SerializeField]
    private List<Memory> list = new List<Memory>();

    public ReadOnlyCollection<Memory> values {
        get {
            return list.AsReadOnly();
        }
    }
}
