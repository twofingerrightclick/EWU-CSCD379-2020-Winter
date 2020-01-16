using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    class FingerPrintEntityBase : EntityBase
    {
        /*   string CreatedBy`
        - `DateTime CreatedOn`
        - `string ModifiedBy`
        - `DateTime ModifiedOn`*/

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }


    }
}
