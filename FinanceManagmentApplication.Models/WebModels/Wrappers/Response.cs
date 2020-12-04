using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.WebModels.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T data)
        {
            
            Data = data;
        }
        public T Data { get; set; }
        
    }
}
