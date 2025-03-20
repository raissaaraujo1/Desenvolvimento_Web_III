using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace PetMongoDB.Models
{
    public class ContextMongoDB
    {
        public static string? ConnectionString { get; set; }
        public static string? Database { get; set; }
        //camada de segurança (true or false) por isso é booleano
        public static bool IsSSL { get; set; }
        public IMongoDatabase _database { get; }

        static ContextMongoDB()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
        public ContextMongoDB()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if(IsSSL)
                {
                    //verificação
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                //instancinado um objeto que será criado nessa váriavel
                var mongoCliente = new MongoClient(settings);
                _database = mongoCliente.GetDatabase(Database);

            }
            //criando uma excessão genérica
            catch(Exception)
            {
                throw new Exception("Não foi possível conectar ao MongoDB");

            }
        }//conexão mongo

        //criando a classe Pet no Mongo
        public IMongoCollection <Pet> Pet
        {
             get
            {
                return _database.GetCollection<Pet>("Pet");
            }
        }

    }//fim classe

}//fim namespace
