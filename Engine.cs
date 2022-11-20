using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcingYamaha
{

    public class Engine
    {
        public Menudata menuData { get; set; }
        public Productdatacollection[] productDataCollection { get; set; }
        public Modelnamedatacollection[] modelNameDataCollection { get; set; }
        public Displacementdatacollection[] displacementDataCollection { get; set; }
        public object modelYearDataCollection { get; set; }
        public Usercontext userContext { get; set; }
    }

    public class Model
    {
        public Modelnamedatacollection[] modelNameDataCollection { get; set; }
    }

    public class Menudata
    {
        public object relatedLinkDataCollection { get; set; }
        public bool contactUsFlag { get; set; }
        public string contactUsURL { get; set; }
        public string helpURL { get; set; }
        public bool relatedLinkFlag { get; set; }
    }

    public class Usercontext
    {
        public string dateFormat { get; set; }
        public bool termOfUseFlag { get; set; }
        public string termOfUseURL { get; set; }
        public string destination { get; set; }
        public bool useProdCategory { get; set; }
        public string userGroupCode { get; set; }
        public string destGroupCode { get; set; }
        public bool greyModelSign { get; set; }
        public string domOvsId { get; set; }
        public string cataPBaseCode { get; set; }
        public string priceDisplayType { get; set; }
        public bool stockDisplay { get; set; }
        public int pickListPartCnt { get; set; }
        public string boundSymbol { get; set; }
        public bool taxDisplay { get; set; }
        public bool mobileAvailableFlag { get; set; }
        public string currencyCode { get; set; }
    }

    public class Productdatacollection
    {
        public string vinNoLabel { get; set; }
        public string vinNoLabel2 { get; set; }
        public string modelLabelSearchHelpURL { get; set; }
        public string vinNoGuideMessage { get; set; }
        public string vinNoSearchHelpURL { get; set; }
        public string vinNoSearchLabel { get; set; }
        public string productId { get; set; }
        public string modelLabelSearchId { get; set; }
        public string modelNameSearchId { get; set; }
        public string productIdName { get; set; }
        public string vinNoSearchId { get; set; }
    }

    public class Modelnamedatacollection
    {
        public object nickname { get; set; }
        public string modelName { get; set; }
        public string dispModelName { get; set; }
        public string productId { get; set; }
    }

    public class Displacementdatacollection
    {
        public string productId { get; set; }
        public string displacementType { get; set; }
        public string displacement { get; set; }
    }

}
