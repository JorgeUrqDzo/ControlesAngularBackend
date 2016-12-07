using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiControles.Models
{
    public class Response
    {

        public bool Success { get; set; }
        public object data { get; set; }
    }
}