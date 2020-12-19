using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class FinanceAction: IEntity
    {
        public int Id { get; set; }

        public DateTime ActionDate { get; set; }

        public int Sum { get; set; }

        public Operation Operation { get; set; }

        public int OperationId { get; set; }

        public Project Project { get; set; }

        public int ProjectId { get; set; }

        public Score Score { get; set; }

        public int ScoreId { get; set; }

      //  public virtual User User { get; set; }
        public  User User { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public string Discriminator { get; set; }

        public bool IsDelete { get; set; }
    }
}
