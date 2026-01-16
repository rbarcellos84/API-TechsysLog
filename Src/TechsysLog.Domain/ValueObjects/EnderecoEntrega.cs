using TechsysLog.Domain.Exceptions;

namespace TechsysLog.Domain.ValueObjects
{
    public sealed class EnderecoEntrega
    {
        public string Cep { get; }
        public string Rua { get; }
        public string Numero { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Estado { get; }

        public EnderecoEntrega(string cep, string rua, string numero, string bairro, string cidade, string estado)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new DomainException("CEP é obrigatório");

            if (string.IsNullOrWhiteSpace(rua))
                throw new DomainException("Rua é obrigatória");

            if (string.IsNullOrWhiteSpace(numero))
                throw new DomainException("Número é obrigatório");

            if (string.IsNullOrWhiteSpace(bairro))
                throw new DomainException("Bairro é obrigatório");

            if (string.IsNullOrWhiteSpace(cidade))
                throw new DomainException("Cidade é obrigatória");

            if (string.IsNullOrWhiteSpace(estado))
                throw new DomainException("Estado é obrigatório");

            Cep = cep;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not EnderecoEntrega other) return false;

            return Cep == other.Cep &&
                   Rua == other.Rua &&
                   Numero == other.Numero &&
                   Bairro == other.Bairro &&
                   Cidade == other.Cidade &&
                   Estado == other.Estado;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cep, Rua, Numero, Bairro, Cidade, Estado);
        }

        public override string ToString()
        {
            return $"{Rua}, {Numero} - {Bairro}, {Cidade}/{Estado}, CEP: {Cep}";
        }
    }
}
