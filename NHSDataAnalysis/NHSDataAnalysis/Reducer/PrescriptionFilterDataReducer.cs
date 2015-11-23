using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataAnalysis.Reducer
{
    public class PrescriptionFilterDataReducer : ReducerCombinerBase
    {
        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            int numberOfItems = 0;
            double totalPrice = 0.0d;

            foreach (string price in values)
            {
                decimal value;
                if (Decimal.TryParse(price, out value))
                {
                    // It's a decimal
                    numberOfItems++;
                    totalPrice += (double)value;
                }
                else
                {
                    // No it's not.
                    numberOfItems++;
                    totalPrice += (double)0;
                }
            }

            // Pass the average actual cost of all peppermint oil to output
            string combinedText = "Average Actual Cost of is " + Math.Round(totalPrice / numberOfItems, 2) + ", for " +
               numberOfItems + " Items";

            context.EmitKeyValue(key, combinedText);
        }
    }
}
