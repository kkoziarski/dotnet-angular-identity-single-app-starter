using System;
using CleanArchWeb.Domain.Entities;

namespace CleanArchWeb.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }

    public abstract class AuditableDocument : AuditableEntity, IDocument
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Version { get; set; }
    }
}
