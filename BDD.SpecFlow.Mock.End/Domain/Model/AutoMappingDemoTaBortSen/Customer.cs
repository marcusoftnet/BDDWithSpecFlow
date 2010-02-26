namespace Avega.BDD.SpecFlow.Domain.Model.AutoMappingDemoTaBortSen
{
    public class Customer : EntityBase<Customer>
    {
        public virtual string FirstName {get; set; }
        public virtual string LastName { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string City { get; set; }
        public virtual string CountryCode { get; set; }
    }
}