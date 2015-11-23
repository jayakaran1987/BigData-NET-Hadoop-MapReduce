using NHSDataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataService.Interface
{
    public interface IPrescriptionLineReader
    {
        Prescription ExtractPrescriptionsFromCsvLineFormat(string lineAsCsv);
    }
}
