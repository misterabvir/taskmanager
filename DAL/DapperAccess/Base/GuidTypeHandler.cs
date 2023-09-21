using Dapper;
using System.Data;
using System;

namespace DAL.DapperAccess.Base
{
    internal class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            if (value is string stringValue)
            {
                return Guid.Parse(stringValue);
            }
            throw new ArgumentException("Invalid GUID value");
        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }
    }
}