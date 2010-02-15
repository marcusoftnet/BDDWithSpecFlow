namespace Avega.BDD.SpecFlow.Domain.Model.AutoMappingDemoTaBortSen
{
    public class Employee : EntityBase<Employee>
    {
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
    }
}