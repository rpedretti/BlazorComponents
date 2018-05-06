using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.NHContext
{
    public abstract class BaseNHContext
    {
        private ISessionFactory _sessionFactory;

        public BaseNHContext(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public T WithAutoTransaction<T>(Func<ISession, T> action)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                return action(session);
            }
        }

        public async Task<T> WithSessionAsync<T>(Func<ISession, Task<T>> action)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return await action(session);
            }
        }
    }
}
