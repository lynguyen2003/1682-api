using Models.DTO.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
