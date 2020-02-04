using System;

namespace MainLibrary.database.orm
{
    public class SPNamesAttribute : Attribute
    {
        const string c_defaultDeleteParamName = "ID";

        string _read;
        string _delete;
        string _add;
        string _update;
        string _deleteParamName = c_defaultDeleteParamName;
        bool _noUseOfName;
        
        public string Read { get { return _read; } set { _read = value; } }
        public string Delete { get { return _delete; } set { _delete = value; } }
        public string Add { get { return _add; } set { _add = value; } }
        public string Update { get { return _update; } set { _update = value; } }
        public bool NoUseOfName { get { return _noUseOfName; } set { _noUseOfName = value; } }
        public string DeleteParamName { get { return _deleteParamName; } set { _deleteParamName = value; } }
    };
}
