using System;

namespace CleanArchWeb.Infrastructure.Persistence.Management.Models
{
    public class IndexCreationOptions
    {
        public bool? Unique { get; set; }

        public int? TextIndexVersion { get; set; }

        public int? SphereIndexVersion { get; set; }

        public bool? Sparse { get; set; }

        public string Name { get; set; }

        public double? Min { get; set; }

        public double? Max { get; set; }

        public string LanguageOverride { get; set; }

        public TimeSpan? ExpireAfter { get; set; }

        public string DefaultLanguage { get; set; }

        public int? Bits { get; set; }

        public bool? Background { get; set; }

        public int? Version { get; set; }
    }
}
