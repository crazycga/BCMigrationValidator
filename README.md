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
Connecting to your environment requires local / network access to the database.  The appsettings.json file is the current method used to house the credentials (this will change in the future, when I figure out better ways to do it.)  The following settings are required:

|Setting|Use|Default|
|---|---|---|
|LogLevel|*currently unused*|Debug|
|DefaultServer|Server location|*none*|
|DefaultDatabase|DYNAMICS database name|*none*|
|DefaultUser|A SQL user with access to read the DYNAMICS database tables|*none*|
|DefaultPassword|Password for SQL user|*none*|

# Build and Test
This should build properly using Visual Studio 2022, in .NET 6.

# Contribute
I am open to and welcome any suggestions or contributions.  My intent is to create a validation routine that might help others avoid having to place a call with Microsoft during migration from Dynamics GP to Dynamics 365 Business Central.  I especially welcome any knowledge transfer or information about how the cloud migration works, and where my assumptions might be wrong.  And I welcome any feedback on coding style.  I am an accountant by training, a self-taught (mostly, some official training) programmer, and handle integrations mostly.