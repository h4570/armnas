using System.Collections.Generic;

namespace OSCommander.Models.Samba
{
    public class SambaEntry
    {

        public SambaEntry() { }

        public SambaEntry(string name)
        {
            Name = name;
            Params = new List<KeyValuePair<string, string>>();
        }

        public string Name { get; set; }
        public List<KeyValuePair<string, string>> Params { get; set; }
    }
}
