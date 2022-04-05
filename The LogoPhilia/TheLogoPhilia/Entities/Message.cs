using TheLogoPhilia.ApplicationEnums;


namespace TheLogoPhilia.Entities
{
    public class Message : BaseEntity
    {
        public string MessageContent{get;set;}
        public string MessageSubject{get;set;}
        public  MessageType MessageType{get;set;}
    }
}