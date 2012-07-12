﻿using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Orchard.Data.Conventions
{
    /// <summary>
    /// This attribute is used to mark relationships which need to be eagerly fetched with the parent object,
    /// thus defining an aggregate in terms of DDD
    /// </summary>
    public class AggregateAttribute : Attribute
    {
    }

    public class ReferenceConvention : IReferenceConvention, IReferenceConventionAcceptance, IHasManyConvention, IHasManyConventionAcceptance
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Fetch.Join();
        }

        public void Accept(IAcceptanceCriteria<IManyToOneInspector> criteria)
        {
            // todo
            // criteria.Expect(x => x.Property != null && x.Property.IsDefined(typeof(AggregateAttribute), false));
        }
    
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Fetch.Select();
            instance.Cache.NonStrictReadWrite();
        }

        public void Accept(IAcceptanceCriteria<IOneToManyCollectionInspector> criteria)
        {
            criteria.Expect(x => x.Member != null && x.Member.IsDefined(typeof(AggregateAttribute), false));
        }
    }
}

