using NETStandard.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace NETStandard.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeString { get; } = new List<string>();

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }

        protected virtual void AddInclude(Expression<Func<T, object>> include)
        {
            this.Includes.Add(include);
        }

        protected virtual void AddInclude(string include)
        {
            IncludeString.Add(include);
        }
    }
}
