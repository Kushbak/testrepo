﻿using FinanceManagmentApplication.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.CounterPartiesModel
{
    public class CounterPartyCreateModel
    {
        public string Name { get; set; }

        public bool IsCompany { get; set; }

        public int? UserId { get; set; }

        public List<UserIndexModel> Users { get; set; }
    }
}
