using System;
using System.Collections.Generic;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.Models
{
    public class RoleViewModel
    {
        public int Id{get;set;}
        public string RoleName{get;set;}
        
    }
    public class CreateRoleRequestModel
    {
        public string RoleName{get;set;}   
    }
    public class UpdateRoleRequestModel
    {
        public string RoleName{get;set;}
    }
}