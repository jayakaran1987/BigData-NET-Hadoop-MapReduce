using NHSDataModel.Model;
using NHSDataService.Interface;
using NHSDataService.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataTest
{
    [TestFixture]
    public class PrescriptionLineReaderTests
    {
        // Initialize file reader
        private IPrescriptionLineReader lineReader;

        [SetUp]
        public void SetUp()
        { 
            this.lineReader = new PrescriptionLineReader();
        }

        [Test]
        public void ReadPrescriptionFromCsvReturnCorrectData()
        {
            string filepath = "PrescriptionTest.csv";
           //Q30,5D7,A86021,0102000T0,Peppermint Oil                          ,0000012,00000088.55,00000081.98,201109                                 
 

            var prescription = new Prescription();
            using (var sr = new StreamReader(filepath))
            {
                string line = null;
                //Read and display lines from the file until the end of the file is reached.                
                while ((line = sr.ReadLine()) != null)
                {
                    prescription = lineReader.ExtractPrescriptionsFromCsvLineFormat(line);
                }
            }
            Assert.That(prescription.PracticeId, Is.EqualTo("A86021"));
            Assert.That(prescription.BNFName, Is.EqualTo("Peppermint Oil"));
            Assert.That(prescription.Items, Is.EqualTo(12));
            Assert.That(prescription.ActualCost/100, Is.EqualTo(81.98));
            
        }
    }
}
