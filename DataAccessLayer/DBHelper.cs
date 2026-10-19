namespace DataAccessLayer;

/// <summary>
/// Holds the existing Supabase connection settings in one place for the
/// application's singleton NpgsqlDataSource.
/// </summary>
public static class DBHelper
{
    public const string ConnectionString = @"
        Host=aws-0-ap-northeast-2.pooler.supabase.com;
        Port=5432;
        Database=postgres;
        Username=postgres.ydihqbigntggnohaogxf;
        Password=Sibghat@123;
        SSL Mode=Require;
        Trust Server Certificate=true;
        Timeout=15;
        Command Timeout=30;";
}
