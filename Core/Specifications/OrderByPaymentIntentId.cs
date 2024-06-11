using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderByPaymentIntentId : BaseSpecification<Order>
    {
        public OrderByPaymentIntentId(string paymentIntentId)
         :base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}