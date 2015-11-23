using Microsoft.Hadoop.MapReduce;
using NHSDataAnalysis.Mapper;
using NHSDataAnalysis.Reducer;
using NHSDataService.Interface;
using NHSDataService.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSDataAnalysis
{
    public class GenerateStatistics
    {
        // Main Method
        static void Main(string[] args)
        {
           
            // Load Data in to HDInsight 
            HadoopJobConfiguration practice_jobConfig = new HadoopJobConfiguration()
            {
                InputPath = "/user/Jayakaran/Input/NHS/Practices",
                OutputFolder = "/user/Jayakaran/Output/NHS/Practices",
                DeleteOutputFolder = true
            };
            HadoopJobConfiguration prescription_jobConfig = new HadoopJobConfiguration()
            {
                InputPath = "/user/Jayakaran/Input/NHS/Prescription",
                OutputFolder = "/user/Jayakaran/Output/NHS/Prescription",
                DeleteOutputFolder = true
            };

            HadoopJobConfiguration combined_jobConfig = new HadoopJobConfiguration()
            {
                InputPath = "/user/Jayakaran/Input/NHS/Combined",
                OutputFolder = "/user/Jayakaran/Output/NHS/Combined",
                DeleteOutputFolder = true
            };

            // Call Jobs 

            // Question 1 How many practices are in London?
            Hadoop.Connect().MapReduceJob.Execute<PracticesCountDataMapper, PracticesCountDataReducer>(practice_jobConfig);

            // Question 2 What was the average actual cost of all peppermint oil prescriptions?
            Hadoop.Connect().MapReduceJob.Execute<PrescriptionFilterDataMapper, PrescriptionFilterDataReducer>(prescription_jobConfig);

            // Question 3 Which 5 post codes have the highest actual spend, and how much did each spend in total?
            Hadoop.Connect().MapReduceJob.Execute<CombinedDataMapper, CombinedDataReducer>(combined_jobConfig);

            System.Console.Read();  //using to catch console
        }
       
    }
}
