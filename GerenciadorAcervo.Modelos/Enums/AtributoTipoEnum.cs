using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Enums
{
    public enum AtributoTipoEnum
    {
        Texto = 0,
        Inteiro = 1,
        Decimal = 2,
        [Description("Texto Longo")]
        TextoLongo = 3,
        [Description("Imagem - string base64")]
        Imagem = 4,
        [Description("Data - Formato dd/MM/yyyy")]
        Data = 5
    }
}