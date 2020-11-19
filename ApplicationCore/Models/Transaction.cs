﻿using System;

namespace ApplicationCore.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public Enum Status { get; set; }
    }
}