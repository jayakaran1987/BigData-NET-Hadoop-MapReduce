using NHSDataModel.Model;
using NHSDataService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataService.Service
{
    public class PracticeLineReader : IPracticeLineReader
    {
        //--Line Reader for extract practice data ------------//
        //-----------------------------------------------------

        private string workingText;

        public Practices ExtractPracticesFromCsvLineFormat(string lineAsCsv)
        {
            workingText = lineAsCsv;
            Practices practice = new Practices();

            parseNextValueAsString();
            
            practice.ReferenceId = parseNextValueAsString().Trim();
            practice.Name = parseNextValueAsString().Trim();

            parseNextValueAsString();

            practice.Address = parseNextValueAsString().Trim();
            practice.City = parseNextValueAsString().Trim();
            practice.County = parseNextValueAsString().Trim();
            practice.PostCode = parseNextValueAsString().Trim();

            return practice;
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
    }
}
