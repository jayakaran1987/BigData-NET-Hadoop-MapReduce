using Microsoft.Hadoop.MapReduce;
using NHSDataModel.Model;
using NHSDataService.Interface;
using NHSDataService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataAnalysis.Mapper
{
    public class PrescriptionFilterDataMapper : MapperBase
    {
        private IPrescriptionLineReader reader;
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger
        //(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override void Initialize(MapperContext context)
        {
            reader = new PrescriptionLineReader();
            base.Initialize(context);
        }
        public override void Map(string inputLine, MapperContext context)
        {
            //Step to Identify the Practice or Prescription data set
            //Using the SDK, tried to use the MapperContext.InputFileName property: it is always empty
            //So decided to use items count of each datasets
            //Items count 8 for practice and Items count 9 for prescription

            char[] delimiterChars = { ',' };

            //split up the passed in line
            string[] individualItems = inputLine.Trim().Split(delimiterChars);

            if (individualItems.Count() == 9)
            {
                Prescription prescription = reader.ExtractPrescriptionsFromCsvLineFormat(inputLine);

                if (String.IsNullOrWhiteSpace(prescription.PracticeId)) { return; }  //Ignore, practise name cannot be null

                //Filter by peppermint oil

                if (prescription.BNFName.ToLower() != "peppermint oil") { return; }  //Ignore, if filtor not matched

                context.EmitKeyValue(prescription.BNFName, prescription.ActualCost.ToString("0.00"));
            } 
        }
    }
}
