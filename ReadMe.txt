Ouestion

The below links are to datasets containing NHS data. You will find data on UK practices and their prescriptions prescribed in a year. 

http://datagov.ic.nhs.uk/T201202ADD%20REXT.CSV 
http://datagov.ic.nhs.uk/T201109PDP%20IEXT.CSV 

We would like you to build and submit a solution that parses these datasets and answers the following questions: 
1. How many practices are in London? 
2. What was the average actual cost of all peppermint oil prescriptions? 
3. Which 5 post codes have the highest actual spend, and how much did each spend in total? 
4. For each region of England (North East, South West, London, etc.): 
a. What was the average price per prescription of Flucloxacillin (excluding Co-Fluampicil)? 
b. How much did this vary from the national mean? 
5. Come up with your own interesting question about NHS prescriptions in England and use the data (plus any other sources you'd like to use) to answer it. 

My Approach
After having the first look, I have decided that the solution should be fixed by big data approach. 
Since I’m experienced in C#, I decided to go forward with Hadoop library with Microsoft.Hadoop.MapReduce.
I don’t have any practical experience with Big data, I referenced following useful startup tutorials to configure HDInsight, Hadoop and writing MapReduce functions in C#

Useful Links
1.	https://azure.microsoft.com/en-gb/documentation/articles/hdinsight-hadoop-emulator-get-started/
2.	https://azure.microsoft.com/en-gb/documentation/articles/hdinsight-hadoop-develop-deploy-streaming-jobs/
3.	https://www.youtube.com/watch?v=uyi41nrhlhw&feature=youtu.be
4.	https://martin.atlassian.net/wiki/pages/viewpage.action?pageId=10354721

Improvements need to fix in existing code – future commits will cover these
1.	Chain MapReduce for Multiple Jobs
    https://hadoop.apache.org/docs/r2.7.0/api/org/apache/hadoop/mapred/lib/ChainMapper.html
    https://hadoop.apache.org/docs/r2.7.0/api/org/apache/hadoop/mapred/lib/ChainReducer.html
2.	Efficient Sorting mechanism, Combine/join two datasets in efficient way, Sort Comparator and Group Comparator 
3.	The fourth question needs an additional datasets to find all the Postcodes inside Region. So that grouped Postcodes results goes to the Region based results
4.	Using the SDK, tried to use the MapperContext.InputFileName property: it is always empty. Need to find the clean way to distinguish datasets in Mapper
