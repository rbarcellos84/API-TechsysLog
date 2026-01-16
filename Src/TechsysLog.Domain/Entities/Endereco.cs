namespace TechsysLog.Domain.Entities
{
    public class Endereco
    {
        public string CEP { get; private set; }
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public Endereco(string cep, string rua, string numero, string bairro, string cidade, string estado)
        {
            CEP = cep;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
    }
}
