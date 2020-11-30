using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Document
    {
        public OrderedDictionary Fields { get; set; }

        public Document()
        {
            Fields = new OrderedDictionary();
        }
    }
}
