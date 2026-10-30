
using Microsoft.Data.Sqlite;

string connectionString = "Data Source=students.db";

using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Students (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            StudentNumber TEXT NOT NULL UNIQUE,
            Name TEXT NOT NULL,
            Course TEXT NOT NULL
        );";

    command.ExecuteNonQuery();
}

bool running = true;

while (running)
{
    Console.WriteLine("\n=== STUDENT RECORD SYSTEM ===");
    Console.WriteLine("1. Register Student");
    Console.WriteLine("2. View Students");
    Console.WriteLine("3. Search Student");
    Console.WriteLine("4. Update Student");
    Console.WriteLine("5. Delete Student");
    Console.WriteLine("6. Exit");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();

    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    using var command = connection.CreateCommand();

    if (choice == "1")
    {
        Console.Write("Enter student number: ");
        string studentNumber = Console.ReadLine() ?? "";

        Console.Write("Enter student name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter course: ");
        string course = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(studentNumber) ||
            string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(course))
        {
            Console.WriteLine("All fields are required.");
            continue;
        }

        command.CommandText = @"
            INSERT INTO Students (StudentNumber, Name, Course)
            VALUES ($number, $name, $course);";

        command.Parameters.AddWithValue("$number", studentNumber);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$course", course);

        try
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Student registered successfully!");
        }
        catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
        {
            Console.WriteLine("That student number already exists.");
        }
    }
    else if (choice == "2")
    {
        command.CommandText = "SELECT * FROM Students";

        using var reader = command.ExecuteReader();

        Console.WriteLine("\nID | STUDENT NUMBER | NAME | COURSE");
        Console.WriteLine("------------------------------------------");

        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader["Id"]} | {reader["StudentNumber"]} | " +
                $"{reader["Name"]} | {reader["Course"]}");
        }
    }
    else if (choice == "3")
    {
        Console.Write("Enter student number to search: ");
        string number = Console.ReadLine() ?? "";

        command.CommandText =
            "SELECT * FROM Students WHERE StudentNumber = $number";
        command.Parameters.AddWithValue("$number", number);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            Console.WriteLine($"ID: {reader["Id"]}");
            Console.WriteLine($"Name: {reader["Name"]}");
            Console.WriteLine($"Course: {reader["Course"]}");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }
    else if (choice == "4")
    {
        Console.Write("Enter student number to update: ");
        string number = Console.ReadLine() ?? "";

        Console.Write("Enter new name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter new course: ");
        string course = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(course))
        {
            Console.WriteLine("Name and course are required.");
            continue;
        }

        command.CommandText = @"
            UPDATE Students
            SET Name = $name, Course = $course
            WHERE StudentNumber = $number;";

        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$course", course);
        command.Parameters.AddWithValue("$number", number);

        int rows = command.ExecuteNonQuery();

        Console.WriteLine(rows > 0
            ? "Student updated successfully!"
            : "Student not found.");
    }
    else if (choice == "5")
    {
        Console.Write("Enter student number to delete: ");
        string number = Console.ReadLine() ?? "";

        Console.Write("Are you sure? (yes/no): ");
        string confirmation = Console.ReadLine() ?? "";

        if (confirmation.Equals("yes", StringComparison.OrdinalIgnoreCase))
        {
            command.CommandText =
                "DELETE FROM Students WHERE StudentNumber = $number";
            command.Parameters.AddWithValue("$number", number);

            int rows = command.ExecuteNonQuery();

            Console.WriteLine(rows > 0
                ? "Student deleted successfully!"
                : "Student not found.");
        }
        else
        {
            Console.WriteLine("Deletion cancelled.");
        }
    }
    else if (choice == "6")
    {
        running = false;
        Console.WriteLine("Exiting the system. Goodbye!");
    }
    else
    {
        Console.WriteLine("Invalid option. Please choose between 1 and 6.");
    }
}
