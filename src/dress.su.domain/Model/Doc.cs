using System;

using MainLibrary.database.orm;
using sys.Time;

namespace dress.su.domain.Model
{
    [
        SPNames
        (
            Add="insert into doc (id, \"type\") values (null, @in_type)",
            Read="select * from doc where id = @in_id",
            Update="update doc set \"time_cancelled\" = @in_timeCancelled where id = @in_id",
            Delete="delete from doc where id = @in_id",
            DeleteParamName="@in_id",
            NoUseOfName=true
        )
    ]
    public class Doc: BizObject<Doc>
    {
        public enum DocType
        {
            Sale = 1
        };

        protected DocType   _type;
        protected DateTime  _timeCreated;
        protected DateTime? _timeCancelled;

        protected override void FillProps(System.Data.DataRow in_row)
        {
            base.FillProps(in_row);
            _type = (DocType)(long)in_row["type"];
            _timeCreated = UnixEpoch.ToDateTime((long)in_row["time_created"]);
            if ((in_row["time_cancelled"] as long?).HasValue)
                _timeCancelled = UnixEpoch.ToDateTime((long)in_row["time_cancelled"]);
        }
        protected override void FillUpdateParams(CustomSqlCommand in_sp)
        {
            const string c_timeCancelledParam = "@in_timeCancelled";

            base.FillUpdateParams(in_sp);
            in_sp.AddParameter("@in_type", (long)_type);
            if (_timeCancelled.HasValue)
                in_sp.AddParameter(c_timeCancelledParam, UnixEpoch.FromDateTime(_timeCancelled.Value));
            else
                in_sp.AddParameter(c_timeCancelledParam, (long?)null);
        }

        public static Doc CreateNew(DocType in_type)
        {
            Doc result = new Doc();
            result._type = in_type;
            return result;
        }

        public DocType Type { get { return _type; } }
        public DateTime TimeCreated { get { return _timeCreated; } }
        public DateTime? TimeCancelled { get { return _timeCancelled; } set { Modify(); _timeCancelled = value; } }
        public bool IsCancelled { get { return _timeCancelled.HasValue; } }
    };
}
