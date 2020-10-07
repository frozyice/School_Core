using System.Collections.Generic;
using School_Core.Domain.Models;
using School_Core.Specifications;

namespace School_Core.Queries
{
    public interface IQuery<TEntity> where TEntity : Entity
    {
        IReadOnlyList<TEntity> GetAll(ISpecification<TEntity> spec = null);
        TEntity GetSingleOrDefault(ISpecification<TEntity> spec);
        

        //todo Mis saab kui sed CanEnroll spekiga kutsuda ? Justkui peaks mingi piirang olema mis tüüpoi spekiga seda üldse kutsuda saab. Samas las ta selle loo raames olla.
        //Tõenäoliselt me tahame millalgi kindlasti ka lisada context.Find meetodi et kõik need kohad kus me küsimse välja ühe kindla asja ID järgi ei teeks igas uues kihis uut päringut.
    }
}