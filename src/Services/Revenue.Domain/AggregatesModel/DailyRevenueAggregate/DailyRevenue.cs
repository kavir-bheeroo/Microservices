using System;
using System.Collections.Generic;
using Microservices.Services.Revenue.Domain.Seedwork;
using Revenue.Domain.Exceptions;

namespace Microservices.Services.Revenue.Domain.AggregatesModel.DailyRevenueAggregate
{
    public class DailyRevenue : Entity, IAggregateRoot
    {
        public DateTime Date { get; private set; }
        public decimal TotalIncome { get; private set; }

        public DailyRevenue(DateTime date, decimal amount)
        {
            Date = date.Date;
            TotalIncome = amount;
        }

        public void AddIncome(decimal amount)
        {
            if (amount <= 0) throw new RevenueDomainException($"{nameof(amount)} cannot be zero or less.");

            TotalIncome += amount;
        }
    }
}