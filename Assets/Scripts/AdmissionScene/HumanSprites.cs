using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanSprites", menuName = "Scriptable Objects/HumanSprites")]
[System.Serializable]
public class HumanSprites : ScriptableObject {
    [System.Serializable]
    public class Collection {
        [SerializeField]  
        private List<Sprite> _ghostHairs;
        [SerializeField]
        private List<Sprite> _ghostHeads;
        [SerializeField]
        private List<Sprite> _portraitHairs;
        [SerializeField]
        private List<Sprite> _portraitHeads;

        public ReadOnlyCollection<Sprite> GhostHairs {
            get { return _ghostHairs.AsReadOnly(); }
        }
        public ReadOnlyCollection<Sprite> GhostHeads {
            get { return _ghostHeads.AsReadOnly(); }
        }
        public ReadOnlyCollection<Sprite> PortraitHairs {
            get { return _portraitHairs.AsReadOnly(); }
        }
        public ReadOnlyCollection<Sprite> PortraitHeads {
            get { return _portraitHeads.AsReadOnly(); }
        }
    }

    [SerializeField]
    public Collection child;
    [SerializeField]
    public Collection adult;
    [SerializeField]
    public Collection geezer;

    public Collection GetAppropriateCollection(AgeGroup age) {
        switch (age) {
            case AgeGroup.CHILD: return child;
            case AgeGroup.ADULT: return adult;
            case AgeGroup.GEEZER: return geezer;
            default: return null;
        }
    }
}
