using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("tbProdutos")]
    public class Produto
    {
        [Key]
        public int CodProduto { get; set; }
        public string Descricao { get; set; }
        public double Qtde { get; set; }
        public double Preco { get; set; }
        public bool? PromocaoAtiva { get; set; }
    }
}
