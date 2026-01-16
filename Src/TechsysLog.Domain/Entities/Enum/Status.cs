using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechsysLog.Domain.Entities.Enum
{
    public enum Status
    {
        Ingressado = 1,
        Processando = 2,
        Enviado = 3,
        DestinatarioAusente = 4,
        Entregue = 5,
        Cancelado = 6
    }
    public class InformacaoStatus
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
    public static class EstadosStatus
    {
        public static List<InformacaoStatus> ObterListaStatus()
        {
            return new List<InformacaoStatus>
            {
                new InformacaoStatus { Codigo = 1, Descricao = "Ingressado" },
                new InformacaoStatus { Codigo = 2, Descricao = "Processando" },
                new InformacaoStatus { Codigo = 3, Descricao = "Enviado" },
                new InformacaoStatus { Codigo = 4, Descricao = "DestinatarioAusente" },
                new InformacaoStatus { Codigo = 5, Descricao = "Entregue" },
                new InformacaoStatus { Codigo = 6, Descricao = "Cancelado" }
            };
        }

        public static Status? ObterStatusPorCodigo(int codigo)
        {
            return codigo switch
            {
                1 => Status.Ingressado,
                2 => Status.Processando,
                3 => Status.Enviado,
                4 => Status.DestinatarioAusente,
                5 => Status.Entregue,
                6 => Status.Cancelado,
                _ => null
            };
        }

        public static int ObterPosicaoDoStatus(Status status)
        {
            return (int)status;
        }
    }
}
