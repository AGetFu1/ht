//-----------------------------------------------------------------------
// <copyright file=" SysExpt.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: SysExpt.cs
// * history : Created by T4 11/18/2021 14:52:55 
// </copyright>
//-----------------------------------------------------------------------
using EMS.Data;
using EMS.Domain.Entity.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.IRepository.ExceptionManage
{
    public interface ISysExptRepository : IRepositoryBase<SysExptEntity>
    {
        List<SysExptEntity> GetItemList(string enCode);
        void SubmitForm(SysExptEntity itemsDetailEntity, string keyValue);
    }
}