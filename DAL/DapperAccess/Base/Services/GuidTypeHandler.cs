using Dapper;
using System.Data;
using System;

namespace DAL.DapperAccess.Base
{
    internal class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            if (!Guid.TryParse(value.ToString(), out Guid guid))
                throw new ArgumentException("Error casting string to GUID");
            return guid;
        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }
    }
}