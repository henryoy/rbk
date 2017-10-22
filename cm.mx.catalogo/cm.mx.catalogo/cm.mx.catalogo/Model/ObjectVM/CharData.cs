using rbk.mailrelay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model.ObjectVM
{
    public class CharData
    {
        public CharData()
        {
            lsClickInfo = new List<ResultsUniqueClicks>();
            opensArray = new List<string>();
            clicksArray = new List<string>();
            daysArray = new List<string>();
        }


        public List<string> opensArray { get; set; }
        public List<string> clicksArray { get; set; }
        public List<string> daysArray { get; set; }
        public ResultStatistics oResultStatistics {get;set;}
        public List<ResultsUniqueClicks> lsClickInfo { get; set; }
        public ResultsUniqueClicks oUniqueClicksInfo { get; set; }
        public List<ResultStatistics> lsResultStatistics { get; set; }
        public List<ResultImpressionsInfo> lsResultImpressionsInfo { get; set; }
    }
}
