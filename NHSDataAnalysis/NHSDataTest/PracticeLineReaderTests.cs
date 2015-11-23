using System;
using NUnit.Framework;
using NHSDataService.Interface;
using NHSDataService.Service;
using NHSDataModel.Model;
using System.IO;

namespace NHSDataTest
{
    [TestFixture]
    public class PracticeLineReaderTests
    {
        // Initialize file reader
        private IPracticeLineReader lineReader;

        [SetUp]
        public void SetUp()
        {
            this.lineReader = new PracticeLineReader();
        }

        [Test]
        public void ReadPrescriptionFromCsvReturnCorrectData()
        {
            string filepath = "PracticeTest.csv";
            //201202,A81001,THE DENSHAM SURGERY ,THE HEALTH CENTRE ,LAWSON STREET,STOCKTON ,CLEVELAND,TS18 1HU



            var practice = new Practices();
            using (var sr = new StreamReader(filepath))
            {
                string line = null;
                //Read and display lines from the file until the end of the file is reached.                
                while ((line = sr.ReadLine()) != null)
                {
                    practice = lineReader.ExtractPracticesFromCsvLineFormat(line);
                }
            }
            Assert.That(practice.ReferenceId, Is.EqualTo("A81001"));
            Assert.That(practice.Name, Is.EqualTo("THE DENSHAM SURGERY"));
            Assert.That(practice.PostCode, Is.EqualTo("TS18 1HU"));

        }
    }
}
