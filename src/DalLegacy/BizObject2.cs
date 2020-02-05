using System;
using System.Data;

namespace DalLegacy
{
    public abstract class BizObject2 : BizObject
    {
        protected bool NoUseOfName { get { return ((SPNamesAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(SPNamesAttribute))).NoUseOfName; } }

        protected virtual void FillProps(DataRow in_row)
        {
            Id = (int)(long)in_row["id"];
            if (!NoUseOfName)
                _name = ((string)in_row["name"]).Trim();
        }
    };
}
