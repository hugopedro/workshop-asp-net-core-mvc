using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class IntegrityException : ApplicationException //herdou do ApplicationException
    { //essa vai ser a exceção personalizada de SERVIÇO para erros de integridade referencial(ex:atributo preso à chave estrangeira)
        public IntegrityException(string message) : base(message)
        {
            //serve pra repassar a chamada pra superclasse
        }
    }
}
