using UnityEngine;

namespace System.Collections.Generic
{
    [Serializable]
    public class CharacterInstructionTuple : SerializableKeyValuePair<CharacterClass, string>
    {
        public CharacterInstructionTuple(CharacterClass item1, string item2) : base(item1, item2) { }
    }

    [Serializable]
    public class CharacterInstructionDictionary : SerializableDictionary<CharacterClass, string>
    {
        [SerializeField] private List<CharacterInstructionTuple> _pairs = new List<CharacterInstructionTuple>();

        protected override List<SerializableKeyValuePair<CharacterClass, string>> _keyValuePairs
        {
            get
            {
                var list = new List<SerializableKeyValuePair<CharacterClass, string>>();
                foreach (var pair in _pairs)
                {
                    list.Add(new SerializableKeyValuePair<CharacterClass, string>(pair.Key, pair.Value));
                }
                return list;
            }

            set
            {
                _pairs.Clear();
                foreach (var kvp in value)
                {
                    _pairs.Add(new CharacterInstructionTuple(kvp.Key, kvp.Value));
                }
            }
        }
    }
}