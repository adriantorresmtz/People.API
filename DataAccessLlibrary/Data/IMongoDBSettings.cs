namespace DataAccess.Data
{
    public interface IMongoDBSettings
    {
        string Collection { get; set; }
        string DataBase { get; set; }
        string Server { get; set; }
    }
}