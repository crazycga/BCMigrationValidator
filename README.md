# Introduction 
This project started in late 2022 when trying to migrate a client from Dynamics GP to Dynamics 365 Business Central.  We suffered some issues with data quality and special characters that the mirgation routine failed on, but were not readily apparent in our source data.  This program is meant to assist anyone who is doing one of these migrations.

**I am not a Microsoft employee; I have no "inside" information.  All of this was based on observation of their system.**

# Intended Use
When running an Business Central Cloud Migration, the system creates three tables in your DYNAMICS database:
1. [$ic$Replication] : this table describes the tables that the migraiton tool will bring over
2. [$ic$ReplicationBatch] : this table describes the actions for each of the batches
3. [$ic$ReplicationBatchPartition] : this table contains the data produced for consumption by their system in XML forms

This program will examine the data in these tables and execute tests against that data to expose known weaknesses (and downright blocking issues) in the data that have caused problems before.

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)