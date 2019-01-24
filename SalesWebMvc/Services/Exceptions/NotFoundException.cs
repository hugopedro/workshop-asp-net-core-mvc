using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException // exceção personalizada
    {
        public NotFoundException(string message) :base(message)
        { // exceção personalizada not found
            //isso é pra ter um controle maior sobre cada exceção que poderá ocorrer
        }
    }
}
