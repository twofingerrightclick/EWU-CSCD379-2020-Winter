using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
        private string _Name=string.Empty;
        public string Name {
            get => _Name;
            set => _Name=value ?? throw new ArgumentNullException(nameof(value), "Cannot set to null");
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CA2227 // Collection properties should be read only
        public List<UserGroup> UserGroups { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

    }
}
