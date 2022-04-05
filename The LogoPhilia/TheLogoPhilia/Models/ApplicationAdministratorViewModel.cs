using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Models
{
    public class ApplicationAdministratorViewModel
    {
        public int Id {get;set;}
        public string FullName {get;set;}
        public string AdministratorEmail{get;set;}
        public string AdminImage {get;set;}
        public int UserId{get;set;}
        public string UserName{get;set;}
        public int Age {get;set;}
        public DateTime DateOfBirth{get;set;}
        public string AdministratorCode{get;set;}
        public AdminType AdministratorType{get;set;}
    }
    public class CreateApplicationAdministratorRequestModel
    {
        public string AdminImage {get;set;}
        public string FirstName {get;set;}
        public string LastName{get;set;}
        public string UserName{get;set;}
        [EmailAddress]
        public string AdministratorEmail{get;set;}
        [DataType(DataType.Password)]
        public string Password {get;set;}
        public DateTime DateOfBirth{get;set;}
         
    }
    public class CreateSubAdministratorRequestModel
    {
        public string AdminImage {get;set;}
        public string FirstName {get;set;}
        public string LastName{get;set;}
        public string UserName{get;set;}
        [EmailAddress]
        public string AdministratorEmail{get;set;}
        [DataType(DataType.Password)]
        public string Password {get;set;}
        public DateTime DateOfBirth{get;set;}
    }
    public class UpdateApplicationAdministratorRequestModel
    {
        public string FirstName {get;set;}
        public string UserName {get;set;}
        public string LastName{get;set;}
         public IFormFile AdminImage {get;set;}
        public DateTime DateOfBirth{get;set;}

    }
    
}