using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.FilterModels
{
    public class ExcelFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int[] OperationsId { get; set; }
        public int[] ProjectsId { get; set; }
        public int[] ScoresId { get; set; }
        public int[] CounterPartiesId { get; set; }
        public int[] Scores2Id { get; set; }
        public int[] UsersId { get; set; }
        public int[] OperationTypesId { get; set; }
    }
}
