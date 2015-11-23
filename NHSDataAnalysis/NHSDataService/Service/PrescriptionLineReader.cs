using NHSDataModel.Model;
using NHSDataService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataService.Service
{
    public class PrescriptionLineReader : IPrescriptionLineReader
    {
        //--Line Reader for extract prescription data ------------//
        //-----------------------------------------------------

        private string workingText;

        public Prescription ExtractPrescriptionsFromCsvLineFormat(string lineAsCsv)
        {
            workingText = lineAsCsv;
            Prescription prescription = new Prescription();

            prescription.SHA = parseNextValueAsString().Trim();
            prescription.PCT = parseNextValueAsString().Trim();
            prescription.PracticeId = parseNextValueAsString().Trim();
            prescription.BNFCode = parseNextValueAsString().Trim();
            prescription.BNFName = parseNextValueAsString().Trim();
            prescription.Items = parseNextValueAsInt();
            prescription.NIC = parseNextValueAsDecimal();
            prescription.ActualCost = parseNextValueAsDecimal();
            prescription.Period = parseNextValueAsString();


            return prescription;
        }
        private String parseNextValueAsString()
        {
            string nextValue = null;
            //field, might, or might not, start with double-quotes
            if (workingText.StartsWith("\""))
            {
                int endOfValue = workingText.IndexOf("\"", 1);
                nextValue = workingText.Substring(1, endOfValue - 1);
                workingText = workingText.Substring(endOfValue + 2);
            }
            else
            {
                int endOfValue = workingText.IndexOf(",", 0);

                if (endOfValue != -1)
                {
                    nextValue = workingText.Substring(0, endOfValue);
                    workingText = workingText.Substring(endOfValue + 1);
                }
                else nextValue = workingText;

            }
            return nextValue;
        }

        private decimal parseNextValueAsDecimal()
        {
            String deciamlAsString = parseNextValueAsString();
            deciamlAsString = deciamlAsString.Replace("$", "");
            deciamlAsString = deciamlAsString.Replace(",", "");
            deciamlAsString = deciamlAsString.Replace(" ", "");

            decimal value;
            if (Decimal.TryParse(deciamlAsString, out value))
            {
                // It's a decimal
                return value;
            }
            else
            {
                // No it's not.
                return 0;
            }
                
        }

        private int parseNextValueAsInt()
        {
            String intAsString = parseNextValueAsString();
            intAsString = intAsString.Replace("$", "");
            intAsString = intAsString.Replace(",", "");
            intAsString = intAsString.Replace(" ", "");

            int value;
            if (int.TryParse(intAsString, out value))
            {
                // It's a decimal
                return value;
            }
            else
            {
                // No it's not.
                return 0;
            }
        }


    }
}
