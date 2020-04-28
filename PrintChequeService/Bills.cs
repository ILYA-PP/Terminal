using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PrintChequeService
{
    public class bills
    {
        [XmlElement("bill")]
        public List<Cheque> Cheques { get; set; }
    }
}
