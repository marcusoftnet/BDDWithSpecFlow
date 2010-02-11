namespace Avega.BDD.SpecFlow.Domain.Model.AutoMappingDemoTaBortSen
{
    public class Product : EntityBase<Product>
    {
        public virtual string Name { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual bool Discontinued { get; set; }
    }

}