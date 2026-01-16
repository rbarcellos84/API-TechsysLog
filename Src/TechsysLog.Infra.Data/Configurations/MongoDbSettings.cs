namespace TechsysLog.Infra.Data.Configurations
{
    /// <summary>
    /// Representa as configurações de conexão com o MongoDB.
    /// </summary>
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
