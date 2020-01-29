"# TechnicalTest" 


To test this application:

Create the database
1. Copy script from the DB Script file
2. Run in a new query window in SQL
3. Import the Test_Accounts.csv flatfile into the appropriate table. 

Start the API
1. Open the TechnicalTestAPI Folder
2. Open the sln file.
3. Search for the CONNECTIONSTRING const in the DataLayerAccess project and change it to work for you.
4. Run the solution

Open the web application
1. Open the TechnicalTestWeb Folder
2. Open the sln file.
3. Run the solution
4. Select the meter readings .csv file
5. Press the upload button
6. Validate the correct number of successful/failing entries.
7. You may need to change the APIURL const in the ApiAccess project to work for you (if you use a different port)
