using System;

namespace Avega.BDD.SpecFlow.Domain.Model.AutoMappingDemoTaBortSen
{
    /// <summary>
    /// This entity base class is used to be able to do equality checks
    /// in one place. Otherwise the test will throw a 
    /// System.ApplicationException: Expected 'Product' but got 'ProductProxy....' for Property 'Product' 
    /// when checking references
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityBase<TEntity> where TEntity : EntityBase<TEntity>
    {
        private int? oldHashCode;
        public virtual Guid Id { get; protected set; }

        public virtual bool Equals(EntityBase<TEntity> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override int GetHashCode()
        {
            // Once we have a hash code we'll never change it
            if (oldHashCode.HasValue)
                return oldHashCode.Value;

            var thisIsTransient = Equals(Id, Guid.Empty);

            // When this instance is transient, we use the base GetHashCode()
            // and remember it, so an instance can NEVER change its hash code.
            if (thisIsTransient)
            {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as TEntity;
            if (other == null)
                return false;

            // handle the case of comparing two NEW objects
            var otherIsTransient = Equals(other.Id, Guid.Empty);
            var thisIsTransient = Equals(Id, Guid.Empty);
            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(other, this);

            return other.Id.Equals(Id);
        }
    }
}