using System;

namespace CleanArchWeb.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class CollectionNameAttribute : Attribute
    {
        public CollectionNameAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Empty collection name is not allowed", nameof(name));
            }

            this.Name = name;
        }

        public virtual string Name { get; private set; }
    }
}
