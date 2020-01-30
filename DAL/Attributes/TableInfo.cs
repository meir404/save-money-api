using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Attributes
{
    public class TableInfo: Attribute
    {
        public string Name{ get; set; }
    }
}
