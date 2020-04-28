using System.Xml.Serialization;

namespace PrintChequeService
{
    public class Product
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("price")]
        public double Price { get; set; }
        [XmlAttribute("qnt")]
        public int Quantity { get; set; }
        [XmlAttribute("nds")]
        public double NDS { get; set; }
        [XmlAttribute("nds_summ")]
        public double NDS_Summ { get; set; }
        [XmlAttribute("row_type")]
        public int Row_Type { get; set; }
        [XmlAttribute("row_summ")]
        public double Row_Summ { get; set; }
    }
}
