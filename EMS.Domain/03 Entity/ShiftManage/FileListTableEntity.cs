using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.ViewModel;

namespace EMS.Domain.Entity.ShiftManage
{
    public class FileListTableEntity : IEntity<FileListTableEntity>, ICreationAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_UseName { get; set; }
        public string F_FileName { get; set; }
        public string F_GongXv { get; set; }
        public string F_ItemId { get; set; }
        public DateTime? F_Date { get; set; }
        public string F_Type { get; set; }
        public string F_Address { get; set; }     
        public string F_Description { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public bool? F_DeleteMark { get; set; }
    }
}
