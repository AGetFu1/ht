using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Entity.LogManage
{
    public class OperateLog
    {
        // <summary>
        /// 主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 操作员Id
        /// </summary>
        [DisplayName("操作员Id")]
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        [DisplayName("操作员")]
        public string Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [DisplayName("操作时间")]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 具体操作
        /// </summary>
        [DisplayName("具体操作")]
        public string Operate { get; set; }
        /// <summary>
        /// 重要级别
        /// </summary>
        [DisplayName("重要级别")]
        public int Level { get; set; }
        /// <summary>
        /// 重要级别
        /// </summary>
        [NotMapped]
        [DisplayName("重要级别")]
        public ImportantLevel LevelEnum
        {
            get { return (ImportantLevel)Level; }
            set { Level = (int)value; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; set; }
        /// <summary>
        /// 重要级别
        /// </summary>
        public enum ImportantLevel { 一般操作 = 0, 危险操作 = 1 }
    }
}
