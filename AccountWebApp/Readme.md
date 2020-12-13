here is other version of teh CRUD operation but without using the EF.
1. first create the database in sql server and created the Stored Procedures as well for all the needed operations.
2. based on the database, i have created a model class in the model folder in VS wchic contains all the Properties.
3. creaete t the connectionstring whihc is located in the appsettings.json.
4. the configuration of the connectionstring is added with the use of constructor in accountconroller.
5. while creating the controller, the account view is created in Views with the edit, delete, index,create views.
6. the first function, index to view all the accounts by using the Stored Procedures created var in sql.
7. second fucn is to edit or delete by id.
delete function by id which requires other function to fetch the records from the data by id.(created at the end on the conroller).
8. some modification is been done on the views based on requiremnts.
I have modifed the github igonre to allow it to push the database.mdf.
Finally to be honset(the code took me about 3.5 hours)