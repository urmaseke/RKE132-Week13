


using System.Data.SQLite;
readdata(createconnection());
//InsertCustomer(createconnection());
RemoveCustomer(createconnection());

static SQLiteConnection createconnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data source=mydb.db; Version = 3; New = True; Compress = True;");
    try
    {
        connection.Open();
        Console.WriteLine("DB found.");

    }
    catch
    {
        Console.WriteLine("DB not found.");
    }
    return connection;
}

static void readdata(SQLiteConnection myConnection)
{
Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command=myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerowid = reader["rowid"].ToString();
        string readerstringFirstname = reader.GetString(1);
        string readerstringLastname = reader.GetString(2);
        string readerstringDob = reader.GetString(3);

        Console.WriteLine($"{readerowid}. Full name: {readerstringFirstname} {readerstringLastname}, Dob: {readerstringDob}");

    }
    myConnection.Close();
    
        
}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string Fname, Lname, dob;

    Console.WriteLine("Enter firstname:");
    Fname= Console.ReadLine();
    Console.WriteLine("Enter lastname:");
    Lname= Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy):");
    dob= Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText= $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES  ('{Fname}', '{Lname}', '{dob}')";
   int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");

    readdata(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idTodelete;
    Console.WriteLine("Enter an ID to delete a customer:");
    idTodelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idTodelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");
    readdata(myConnection);

}
