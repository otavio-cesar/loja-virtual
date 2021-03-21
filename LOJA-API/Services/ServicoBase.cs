using Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Servicos
{
    public class ServicoBase<T> where T : class
    {
        public LOJAContext reuniaoContext;

        public ServicoBase(LOJAContext cx)
        {
            reuniaoContext = cx;
        }

        public virtual IList<T> ObterTodos()
        {
            return reuniaoContext.Set<T>().ToList() ;
        }

        public virtual T ObterPorId(int id)
        {
            return reuniaoContext.Set<T>().Find(id);
        }

        public virtual T Salvar(T entity)
        {
            try
            {
                entity = reuniaoContext.Set<T>().Add(entity).Entity;

                reuniaoContext.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual T Atualizar(T entity)
        {
            try
            {
                entity = reuniaoContext.Set<T>().Update(entity).Entity;

                reuniaoContext.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Deletar(int id)
        {
            try
            {
                reuniaoContext.Set<T>().Remove(ObterPorId(id));

                reuniaoContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
