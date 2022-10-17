using MinitoonGames.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinitoonGames.Mappings
{
    public class IntentToDialogDictionary : IIntentToDialogDictionary
    {
        private Dictionary<string, string> Mappings = new Dictionary<string, string>
        {
            { "About", nameof(AboutDialog) },
            {"Show_Games", nameof(ShowGamesDialog)},
        };

        public string this[string key] { get => ((IDictionary<string, string>)Mappings)[key]; set => ((IDictionary<string, string>)Mappings)[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, string>)Mappings).Keys;

        public ICollection<string> Values => ((IDictionary<string, string>)Mappings).Values;

        public int Count => ((IDictionary<string, string>)Mappings).Count;

        public bool IsReadOnly => ((IDictionary<string, string>)Mappings).IsReadOnly;

        public void Add(string key, string value)
        {
            ((IDictionary<string, string>)Mappings).Add(key, value);
        }

        public void Add(KeyValuePair<string, string> item)
        {
            ((IDictionary<string, string>)Mappings).Add(item);
        }

        public void Clear()
        {
            ((IDictionary<string, string>)Mappings).Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)Mappings).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return ((IDictionary<string, string>)Mappings).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((IDictionary<string, string>)Mappings).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IDictionary<string, string>)Mappings).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return ((IDictionary<string, string>)Mappings).Remove(key);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)Mappings).Remove(item);
        }

        public bool TryGetValue(string key, out string value)
        {
            return ((IDictionary<string, string>)Mappings).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, string>)Mappings).GetEnumerator();
        }
    }
}
