using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Models.HelperModel
{
    public class TransactionEditHelperModel
    {
        public Score NewScore { get; set; }

        public Score OldScore { get; set; }

        public int OldOperationTypeId { get; private set; }

        public int NewOperationTypeId { get; private set; }

        public int OldTransactionSum { get; set; }

        public int NewTransactionSum { get; set; }

        public int SetOldOperationTypeId 
        { 
            set 
            {
                if (value == 1 || value == 2)
                    OldOperationTypeId = value;
            } 
        }

        public int SetNewOperationTypeId {
            set 
            {
                if (value == 1 || value == 2)
                    NewOperationTypeId = value;
            }
        }


        public bool IsEditSum {

            get
            {
                return NewTransactionSum != OldTransactionSum;
            }
        }

        public bool IsEditScore
        {
            get
            {
                return OldScore != null && NewScore.Id != OldScore.Id;
            }
        }

        public bool IsEditOperationType
        {
            get
            {
                return NewOperationTypeId != OldOperationTypeId;
            }
        }

        public int GetTransactionDifNew_Old
        {
            get
            {
                return NewTransactionSum - OldTransactionSum;
            }
        }

        public int SetDefaultTransactionSum
        {
            set
            {
                NewTransactionSum = value;
            }
        }

        public int SetDefaultOperationTypeId
        {
            set
            {
                SetNewOperationTypeId = value;
            }
        }

        public Score SetDefaultScore
        {
            set
            {
                NewScore = value;
            }
        }
    }
}
