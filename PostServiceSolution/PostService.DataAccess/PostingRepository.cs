using Microsoft.Data.Sqlite;
using PostService.CommonTypes;
using PostService.Models;

namespace PostService.DataAccess;

public class PostingRepository(string connectionString) : IPostingRepository
{
    private const string Columns =
        "Id, [From], [To], Content, DeliveryType, Weight, Width, Height, Depth, [Value], Price, CreatedAt";

    public void CreateDb()
    {
        using var connection = OpenConnection();
        using var command = new SqliteCommand("""
            CREATE TABLE IF NOT EXISTS Postings (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                [From] TEXT NOT NULL,
                [To] TEXT NOT NULL,
                Content TEXT NOT NULL,
                DeliveryType INTEGER NOT NULL,
                Weight REAL NOT NULL,
                Width INTEGER NOT NULL,
                Height INTEGER NOT NULL,
                Depth INTEGER NOT NULL,
                [Value] REAL NOT NULL,
                Price REAL NOT NULL,
                CreatedAt TEXT NOT NULL
            );
            """, connection);
        command.ExecuteNonQuery();
    }

    public List<Posting> GetList()
    {
        using var connection = OpenConnection();
        using var command = new SqliteCommand($"SELECT {Columns} FROM Postings ORDER BY Id;", connection);
        using var reader = command.ExecuteReader();
        var postings = new List<Posting>();
        while (reader.Read())
        {
            postings.Add(ReadPosting(reader));
        }
        return postings;
    }

    public Posting? GetById(int postingId)
    {
        using var connection = OpenConnection();
        using var command = new SqliteCommand($"SELECT {Columns} FROM Postings WHERE Id = @Id;", connection);
        command.Parameters.AddWithValue("@Id", postingId);
        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadPosting(reader) : null;
    }

    public int Create(Posting posting)
    {
        using var connection = OpenConnection();
        using var command = new SqliteCommand("""
            INSERT INTO Postings ([From], [To], Content, DeliveryType, Weight,
                Width, Height, Depth, [Value], Price, CreatedAt)
            VALUES (@From, @To, @Content, @DeliveryType, @Weight,
                @Width, @Height, @Depth, @Value, @Price, @CreatedAt)
            RETURNING Id;
            """, connection);
        AddPostingParameters(command, posting);
        command.Parameters.AddWithValue("@CreatedAt", posting.CreatedAt.ToString("O"));
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public Posting? Update(Posting posting)
    {
        using var connection = OpenConnection();
        using var command = new SqliteCommand($"""
            UPDATE Postings SET [From] = @From, [To] = @To, Content = @Content,
                DeliveryType = @DeliveryType, Weight = @Weight, Width = @Width,
                Height = @Height, Depth = @Depth, [Value] = @Value, Price = @Price
            WHERE Id = @Id
            RETURNING {Columns};
            """, connection);
        AddPostingParameters(command, posting);
        command.Parameters.AddWithValue("@Id", posting.Id);
        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadPosting(reader) : null;
    }

    public int Delete(int postingId)
    {
        using var connection = OpenConnection();
        using var command = new SqliteCommand("DELETE FROM Postings WHERE Id = @Id;", connection);
        command.Parameters.AddWithValue("@Id", postingId);
        return command.ExecuteNonQuery();
    }

    private SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection(connectionString);
        try
        {
            connection.Open();
            return connection;
        }
        catch
        {
            connection.Dispose();
            throw;
        }
    }

    private static void AddPostingParameters(SqliteCommand command, Posting posting)
    {
        command.Parameters.AddWithValue("@From", posting.From);
        command.Parameters.AddWithValue("@To", posting.To);
        command.Parameters.AddWithValue("@Content", posting.Content);
        command.Parameters.AddWithValue("@DeliveryType", (int)posting.DeliveryType);
        command.Parameters.AddWithValue("@Weight", posting.Weight);
        command.Parameters.AddWithValue("@Width", posting.Width);
        command.Parameters.AddWithValue("@Height", posting.Height);
        command.Parameters.AddWithValue("@Depth", posting.Depth);
        command.Parameters.AddWithValue("@Value", posting.Value);
        command.Parameters.AddWithValue("@Price", posting.Price);
    }

    private static Posting ReadPosting(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32(0),
        From = reader.GetString(1),
        To = reader.GetString(2),
        Content = reader.GetString(3),
        DeliveryType = (DeliveryType)reader.GetInt32(4),
        Weight = reader.GetFloat(5),
        Width = reader.GetInt32(6),
        Height = reader.GetInt32(7),
        Depth = reader.GetInt32(8),
        Value = reader.GetFloat(9),
        Price = reader.GetFloat(10),
        CreatedAt = DateTime.Parse(reader.GetString(11), null, System.Globalization.DateTimeStyles.RoundtripKind)
    };
}
