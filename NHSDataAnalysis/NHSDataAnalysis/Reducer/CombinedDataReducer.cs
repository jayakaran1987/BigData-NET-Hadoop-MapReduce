using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataAnalysis.Reducer
{
    public class CombinedDataReducer : ReducerCombinerBase
    {
        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            double totalPrice = 0.0d;
            string postcode = "";

            foreach (string item in values)
            {
                decimal value;
                if (Decimal.TryParse(item, out value))
                {
                    // It's a actual cost
                    totalPrice += (double)value;
                }
                else
                {
                    // No it's not it is a PostCode.
                    postcode = item;
                }
            }

            
            if (postcode != "")
            {
                // Pass the highest actual spend of each Post Code
                string outputText = "Total Actual Cost in PostCode - " + postcode.Trim() + " Total Cost - " + totalPrice.ToString("0.00");
                context.EmitKeyValue(postcode, outputText);
            }

        }
    }
}
