﻿using Common.Entities;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRequestDetailDL : IBaseDL<RequestDetail>
    {
        /// <summary>
        /// lấy tất cả request detail theo id của cha
        /// </summary>
        /// <param name="overTimeId"></param>
        /// <returns></returns>
        public IEnumerable<RequestDetail> GetAllRecordById(Guid overTimeId);

        /// <summary>
        /// xoá tất cả request detail theo id của cha
        /// </summary>
        /// <param name="overTimeId"></param>
        /// <returns></returns>
        public int DeleteRecordByOverTimeId(Guid overTimeId);

    }
}
