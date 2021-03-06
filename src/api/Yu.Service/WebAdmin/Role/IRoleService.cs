﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Yu.Data.Entities;
using Yu.Model.WebAdmin.Role.InputOuputModels;
using Yu.Model.WebAdmin.Role.OutputModels;

namespace Yu.Service.WebAdmin.Role
{
    public interface IRoleService
    {
        /// <summary>
        /// 取得全部角色名
        /// </summary>
        string[] GetAllRoleNames();

        /// <summary>
        /// 取得角色概要
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="searchText">搜索内容</param>
        PagedData<RoleOutline> GetRole(int pageIndex, int pageSize, string searchText);

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色</param>
        Task<bool> AddRoleAsync(RoleDetail role);

        /// <summary>
        /// 取得角色
        /// </summary>
        /// <param name="id">角色id</param>
        Task<RoleDetail> GetRoleAsync(Guid id);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色id</param>
        Task<bool> DeleteRoleAsync(Guid id);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">角色</param>
        Task UpdateRoleAsync(RoleDetail role);

    }
}
