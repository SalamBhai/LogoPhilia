using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IAdministratorMessageRepository  : IRepository<AdministratorMessage>
    {
        Task<AdministratorMessage> GetAdminMessage(int Id);
        Task<IEnumerable<AdministratorMessage>> GetAdminMessages();

    }
}