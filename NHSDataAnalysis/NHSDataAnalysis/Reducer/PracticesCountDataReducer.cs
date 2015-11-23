using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataAnalysis.Reducer
{
    public class PracticesCountDataReducer : ReducerCombinerBase
    {
        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            // Pass the total Practices count to output
            string outputText = "Total Number of Practises in UK - " + values.Count();
            context.EmitKeyValue(key, outputText);
        }
    }
}
