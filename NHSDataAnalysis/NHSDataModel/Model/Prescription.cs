using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataModel.Model
{
    public class Prescription
    {
        public string SHA { get; set; }
        public string PCT { get; set; }
        public string PracticeId { get; set; }
        public string BNFCode { get; set; }
        public string BNFName  { get; set; }
        public int Items { get; set; }
        public decimal NIC { get; set; }
        public decimal ActualCost { get; set; }
        public string Period { get; set; }
          
    }
}
