using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Data
{
    public class Tag : FingerPrintEntityBase
    {
        public string Name { get; set; }
        public List<PostTag> PostTags { get; set; }
    }
}
