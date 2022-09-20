using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Entity.LogManage
{
    public class OperationLogEntity : IEntity<OperationLogEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public DateTime? F_ModifyTime  { get; set; }

        public string BeforeValue { get; set; }
        public string AfterValue { get; set; }

        public string Username { get; set; }
        public string TableName { get; set; }
        public string ModifyField { get; set; }
        public string ModifyFieldName { get; set; }
        public string IPAddress { get; set; }
        public string LastModifyUserId { get; set; }
        public string F_CreatorUserId { get; set; }
        public string Exception { get; set; }
        public bool? F_DeleteMark { get; set; } 
        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

    }
}
