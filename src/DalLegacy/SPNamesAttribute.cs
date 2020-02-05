using System;

namespace DalLegacy
{
    public class SPNamesAttribute : Attribute
    {
        public string Read { get; set; }

        public string Delete { get; set; }

        public string Add { get; set; }

        public string Update { get; set; }

        public bool NoUseOfName { get; set; }

        public string DeleteParamName { get; set; } = "ID";
    }
}
