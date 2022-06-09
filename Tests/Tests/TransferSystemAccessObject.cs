using System;
using ComponentAccessToDB.RepositoryImplementation;
using ComponentAccessToDB.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Models.ModelsDB;

namespace Tests.Tests
{
    public class TransferSystemAccessObject : IDisposable
    {
        public transfersystemContext TransfersystemContext { get; }
        public ITeamRepository TeamRepository { get; }
        public IPlayerRepository PlayerRepository { get; }
        public IDesiredPlayersRepository DesiredPlayersRepository { get; }

        public TransferSystemAccessObject()
        {
            var builder = new DbContextOptionsBuilder<transfersystemContext>();
            builder.UseInMemoryDatabase("transfersystem");

            TransfersystemContext = new transfersystemContext(builder.Options);
            TeamRepository = new TeamRepository(TransfersystemContext, NullLogger<TeamRepository>.Instance);
            PlayerRepository = new PlayerRepository(TransfersystemContext, NullLogger<PlayerRepository>.Instance);
            DesiredPlayersRepository =
                new DesiredPlayersRepository(TransfersystemContext, NullLogger<DesiredPlayersRepository>.Instance);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                TransfersystemContext.Database.EnsureDeleted();
                TransfersystemContext?.Dispose();
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}