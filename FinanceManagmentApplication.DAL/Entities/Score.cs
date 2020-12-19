using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class Score: IEntity
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public PaymentType PaymentType { get; set; }

        public int PaymentTypeId { get; set; }

        public int Balance { get; set; }

        public bool IsDelete { get; set; }

        public IEnumerable<FinanceAction> FinanceActions { get; set; }

        public IEnumerable<Remittance> Remittances { get; set; }

    }
}
