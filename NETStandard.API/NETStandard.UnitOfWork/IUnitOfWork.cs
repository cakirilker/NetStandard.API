using NETStandard.Repository;
using System;


namespace NETStandard.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        MovieRepository MovieRepository { get; }
        void Commit();
    }
}
