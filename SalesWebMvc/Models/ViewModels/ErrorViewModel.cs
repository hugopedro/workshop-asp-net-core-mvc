using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; } // � pra poder retornar uma mensagem na exce��o

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}