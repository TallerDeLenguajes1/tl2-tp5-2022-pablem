using CadenasDeconexion;

public class CadenaParaSqlite : ICadenaDeConexion
    {
        private readonly IConfiguration configuration;

        public CadenaParaSqlite(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetCadena()
        {
            return configuration["ConnectionString:Default"];
        }
    }