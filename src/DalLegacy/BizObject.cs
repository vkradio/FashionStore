﻿using System;

namespace DalLegacy
{
    public class BizObject : IComparable<BizObject>
    {
        protected const string C_IDPARAM = "@in_id";
        protected const string C_NAMEPARAM = "@in_name";
        protected static readonly DateTime c_minDate = new DateTime(1753, 1, 2);
        protected static readonly DateTime c_maxDate = new DateTime(9999, 12, 30);

        protected bool _inSync = true;
        protected string _name;

        /// <summary>
        /// Primary key
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// Optional parent object
        /// </summary>
        public virtual BizObject Parent { get; set; }
        /// <summary>
        /// Object instance name
        /// </summary>
        public virtual string Name { get { return _name; } set { Modify(); _name = value; } }

        public virtual void Modify() { _inSync = false; }
        public BizObject() { _inSync = false; Id = 0; _name = string.Empty; }
        public BizObject(int in_id, string in_name) { Id = in_id; Name = in_name; }
        public BizObject(int in_id, BizObject in_parent, string in_name) { Id = in_id; Parent = in_parent; _name = in_name; }

        public static bool AreEqual(BizObject in_1, BizObject in_2)
        {
            if (in_1 == null && in_2 == null)
                return true;
            if ((in_1 == null && in_2 != null) || (in_1 != null && in_2 == null))
                return false;
            if (in_1.Id == 0 || in_2.Id == 0)
                return false;
            return in_1.Id == in_2.Id;
        }

        public virtual bool InSync { get { return _inSync; } }

        public override string ToString() { return _name != null ? _name : string.Empty; }

        public int CompareTo(BizObject in_other) { return Name.CompareTo(in_other.Name); }
        public virtual void ResetChildren() { }
        public virtual BizObject GetChild(Guid in_id) { return null; }
        public void SetStorageConsistency() { _inSync = true; }
        public virtual string Validate() { return null; }
    };
}
