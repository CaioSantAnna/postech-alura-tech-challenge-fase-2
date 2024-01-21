using GerenciadorAcervo.Modelos.Dtos;
using GerenciadorAcervo.Modelos.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Funcoes
{
    public static class Funcoes
    {
        public static bool EhImagemValida(string stringBase64)
        {
            Span<byte> buffer = new Span<byte>(new byte[stringBase64.Length]);
            return Convert.TryFromBase64String(stringBase64, buffer, out int bytesParseado);
        }

        public static bool EhInteiroValido(string stringInteiro)
        {
            return Int32.TryParse(stringInteiro, out int inteiroParseado);
        }

        public static bool EhDecimalValido(string stringDecimal)
        {
            return Decimal.TryParse(stringDecimal, new System.Globalization.CultureInfo("pt-BR", false), out decimal decimalParseado);
        }

        public static bool EhDateTimeValido(string stringDateTime)
        {
            return DateTime.TryParse(stringDateTime, new System.Globalization.CultureInfo("pt-BR", false), out DateTime dateTimeParseado);
        }

        public static bool EhAtributoValorValido(AtributoTipoEnum atributoTipo, string valorAtributo)
        {
            switch (atributoTipo)
            {
                case AtributoTipoEnum.Inteiro:
                    return EhInteiroValido(valorAtributo);                    
                case AtributoTipoEnum.Decimal:
                    return EhDecimalValido(valorAtributo);
                case AtributoTipoEnum.Imagem:
                    return EhImagemValida(valorAtributo);
                case AtributoTipoEnum.Data:
                    return EhDateTimeValido(valorAtributo);
                default:
                    return true;
            }
        }
    }
}
