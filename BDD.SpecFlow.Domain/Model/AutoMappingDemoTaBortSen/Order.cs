using System;
using System.Collections.Generic;

namespace Avega.BDD.SpecFlow.Domain.Model.AutoMappingDemoTaBortSen

{
    public class Order : EntityBase<Order>, IAggregateRoot
    {
        public virtual DateTime OrderDate { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<OrderItem> OrderItems { get; set; }
    }
}