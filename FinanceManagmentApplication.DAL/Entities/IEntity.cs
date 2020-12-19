using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public interface IEntity
    {
        public int Id { get; set; }

        public bool IsDelete { get; set; }
    }
}
