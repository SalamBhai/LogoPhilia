using TheLogoPhilia.ApplicationEnums;
using System;

namespace TheLogoPhilia.Models
{
    public class MessageViewModel 
    {
        public int Id{get;set;}
        public string MessageContent{get;set;}
        public  MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}
    }
    public class UpdateMessageRequestModel 
    {
        public string MessageContent{get;set;}
        public  MessageType MessageType{get;set;}
    }
    public class CreateMessageRequestModel 
    {
        public string MessageContent{get;set;}
        public  MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}
    }
}