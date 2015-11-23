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
    public class CombinedDataMapper : MapperBase
    {
        private IPracticeLineReader practiceLinereader;
        private IPrescriptionLineReader prescriptionLinereader;

        public override void Initialize(MapperContext context)
        {
            practiceLinereader = new PracticeLineReader();
            prescriptionLinereader = new PrescriptionLineReader();
            base.Initialize(context);
        }
        public override void Map(string inputLine, MapperContext context)
        {
            char[] delimiterChars = { ','};

            //split up the passed in line
            string[] individualItems = inputLine.Trim().Split(delimiterChars);

            //Step to Identify the Practice or Prescription data set
            //Using the SDK, tried to use the MapperContext.InputFileName property: it is always empty
            //So decided to use items count of each datasets
            //Items count 8 for practice and Items count 9 for prescription

            if (individualItems.Count() == 8)
            {
                Practices practice = practiceLinereader.ExtractPracticesFromCsvLineFormat(inputLine);

                if (String.IsNullOrWhiteSpace(practice.ReferenceId)) { return; }  //Ignore, practise name cannot be null
                if (String.IsNullOrWhiteSpace(practice.Name)) { return; }  //Ignore, practise name cannot be null
                if (String.IsNullOrWhiteSpace(practice.PostCode)) { return; }  //Ignore, practise name cannot be null

                context.EmitKeyValue(practice.ReferenceId, Convert.ToString(practice.PostCode));
            }

            //Items count 9 for prescription

            if (individualItems.Count() == 9)
            {
                Prescription prescription = prescriptionLinereader.ExtractPrescriptionsFromCsvLineFormat(inputLine);

                if (String.IsNullOrWhiteSpace(prescription.PracticeId)) { return; }  //Ignore, practise name cannot be null

               // parctice Id with Amount

                context.EmitKeyValue(prescription.PracticeId, prescription.ActualCost.ToString("0.00"));
            }
           
        }
    }
}
