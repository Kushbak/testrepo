using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Models.HelperModel
{
    public class RemittanceEditHelperModel
    {
        public Score OldScore1 { get; set; }

        public Score NewScore1 { get; set; }

        public Score OldScore2 { get; set; }

        public Score NewScore2 { get; set; }

        public int OldTransactionSum { get; set; }

        public int NewTransactionSum { get; set; }

        public bool IsSumEdit {
            get
            {
                return NewTransactionSum != OldTransactionSum;
            }
        }

        public bool IsScoreEdit
        {
            get
            {
                return NewTransactionSum == OldTransactionSum && (OldScore2 != null || OldScore1 != null);
            }
        }

        public bool IsScore1Edit
        {
            get
            {
                return NewScore1 != null && OldScore1 != null;
            }
        }

        public bool IsScore2Edit
        {
            get
            {
                return NewScore2 != null && OldScore2 != null;
            }
        }

        public int GetTransactionDifNew_Old
        {
            get
            {
                return NewTransactionSum - OldTransactionSum;   
            }
        }

    }
}
