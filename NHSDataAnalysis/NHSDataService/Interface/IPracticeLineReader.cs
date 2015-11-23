using NHSDataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataService.Interface
{
    public interface IPracticeLineReader
    {
        Practices ExtractPracticesFromCsvLineFormat(string lineAsCsv);
    }
}
