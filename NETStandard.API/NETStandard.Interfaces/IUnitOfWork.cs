using NETStandard.Interfaces;
using System;


namespace NETStandard.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IMovieRepository MovieRepository { get; }
        //IDirectorRepository DirectorRepository { get; }
        void Commit();
    }
}
