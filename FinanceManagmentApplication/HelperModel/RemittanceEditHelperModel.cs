using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.HelperModel
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
                return OldScore1 == null && OldScore2 == null;
            }
        }

        public bool IsScoreEdit
        {
            get
            {
                return NewTransactionSum == OldTransactionSum && (OldScore2 != null || OldScore1 != null);
            }
        }

        public bool IsScoreAndSumEdit
        {
            get
            {
                return NewTransactionSum != OldTransactionSum && (OldScore2 != null || OldScore1 != null);
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
