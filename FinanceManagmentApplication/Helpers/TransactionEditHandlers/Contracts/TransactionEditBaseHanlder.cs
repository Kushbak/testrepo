﻿using FinanceManagmentApplication.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Helpers.TransactionEditHandlers.Contracts
{
    public abstract class TransactionEditBaseHanlder
    {
        protected int Expense = 2;
        protected int Income = 1;

        public TransactionEditBaseHanlder Successor { get; set; }
        public abstract void HandleRequest(TransactionEditHelperModel Model);
    }
}
