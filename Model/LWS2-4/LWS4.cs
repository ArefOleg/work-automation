using XML_Header;
using System.Xml.Serialization;
namespace XML_LWS4
{
    public class JETLWS4OrderCancel_Input
    {
        [XmlElement("CardNumber", Namespace = "cusE")]
        public string cardNumber { get; set; }
        [XmlElement("TerminalId", Namespace = "cusE")]
        public string terminalId {get; set; }
        [XmlElement("CardAcceptorId", Namespace = "cusE")]
        public string cardAcceptorId = "CardAcceptorId";
        [XmlElement("AcquiringId", Namespace = "cusE")]
        public string acquiringId = "AcquiringId";
        [XmlElement("RRN", Namespace = "cusE")]
        public string RRN;
        public JETLWS4OrderCancel_Input(){}
    }
    public class Body{
        [XmlElement("JETLWS4OrderCancel_Input", Namespace = "cusE")]
        public JETLWS4OrderCancel_Input jETLWS4OrderCancel_Input;
    }
    [XmlRoot("Envelope", Namespace = "soapenvE")]
    public class LWS4{
        [XmlElement("Header", Namespace = "soapenvE")]
        public Header_LWS header;
        [XmlElement("Body", Namespace = "soapenvE")]
        public Body body;
        

    }
}