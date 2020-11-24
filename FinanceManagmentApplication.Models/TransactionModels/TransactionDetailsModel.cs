using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.TransactionModels
{
    public class TransactionDetailsModel
    {
        public DateTime ActionDate { get; set; }

        public int Sum { get; set; }

        public int OperationId { get; set; }

        public Operation Operation { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int Score1Id { get; set; }

        public Score Score1 { get; set; }

        public int CounterPartyId { get; set; }

        public CounterParty CounterParty { get; set; }

        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public string Description { get; set; }
    }
}
