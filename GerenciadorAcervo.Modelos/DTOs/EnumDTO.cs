using GerenciadorAcervo.Modelos.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Dtos
{
    public class EnumDTO
    {
        public int ID { get { return Convert.ToInt32(_enum); } }
        public string Descricao { get { return _enum.ToDescription(); } }
        private Enum _enum;
        public EnumDTO(Enum Enum)
        {
            _enum = Enum;
        }
    }
}
