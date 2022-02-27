using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class SuccessModel
    {
        public string Titulo { get; set; }
        public int StatusCode { get; set; }
        public string SuccessMensaje { get; set; }
        public object Data { get; set; }
    }
}
