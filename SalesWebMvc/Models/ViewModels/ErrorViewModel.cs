using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; } // É pra poder retornar uma mensagem na exceção

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}