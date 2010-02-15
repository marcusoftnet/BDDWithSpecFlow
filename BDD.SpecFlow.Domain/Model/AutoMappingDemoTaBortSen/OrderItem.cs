namespace Avega.BDD.SpecFlow.Domain.Model.AutoMappingDemoTaBortSen
{
    public class OrderItem 
    {
        public virtual int Id { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Product Product { get; set; }

        public virtual bool Equals(OrderItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id && other.Quantity == Quantity && Equals(other.Product, Product);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Id;
                result = (result * 397) ^ Quantity;
                result = (result * 397) ^ (Product != null ? Product.GetHashCode() : 0);
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(OrderItem)) return false;
            return Equals((OrderItem)obj);
        }
        
    }
}