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
    public class PracticesCountDataMapper : MapperBase
    {
        private IPracticeLineReader reader;

        public override void Initialize(MapperContext context)
        {
            reader = new PracticeLineReader();
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

            if (individualItems.Count() == 8)
            {
                Practices practice = reader.ExtractPracticesFromCsvLineFormat(inputLine);

                if (String.IsNullOrWhiteSpace(practice.ReferenceId)) { return; }  //Ignore, practise name cannot be null
                if (String.IsNullOrWhiteSpace(practice.Name)) { return; }  //Ignore, practise name cannot be null

                context.EmitKeyValue("UK", Convert.ToString(practice.ReferenceId));
            }
           
        }

    }
}
