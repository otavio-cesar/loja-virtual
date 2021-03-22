using Model.Servicos;
using Models.Context;
using Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Servicos
{
    public class ProdutoServico : ServicoBase<Produto>
    {
        public ProdutoServico(LOJAContext cx) : base(cx) { }

        public virtual IList<Produto> ObterTodosAtivos()
        {
            return base.ObterTodos().Where(p => p.PromocaoAtiva ?? false == true).ToList();
        }

        public override Produto Salvar(Produto produto)
        {
            produto.PromocaoAtiva = true;
            return base.Salvar(produto);
        }

        public override void Deletar(Produto produto)
        {
            produto.PromocaoAtiva = false;
            base.Atualizar(produto);
        }
    }
}
