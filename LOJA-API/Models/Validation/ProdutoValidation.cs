using System.Collections.Generic;
using static Models.Validation.WrapperValidation;

namespace Models.Validation
{
    public class ProdutoValidation : InterfaceValidation
    {
        public void validate(dynamic produto)
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(produto.Descricao))
            {
                erros.Add("Descrição não informada.");
            }

            if (produto.Qtde < 0)
            {
                erros.Add("Quantidade deve ser maior que zero.");
            }

            if (produto.Preco < 0)
            {
                erros.Add("Preço deve ser maior que zero.");
            }

            if (erros.Count > 0)
                throw new ValidationException(string.Join(" ", erros).Trim());
        }
    }
}
