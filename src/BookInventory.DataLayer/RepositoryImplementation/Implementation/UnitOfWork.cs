using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using BookInventory.DataLayer.RepositoryImplementation.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookInventory.DataLayer.RepositoryImplementation.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public IServiceProvider ServiceProvider { get; }
        protected readonly BookInventoryDbContext Context;
        private bool _isDisposed;

        public UnitOfWork(BookInventoryDbContext context, IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Context = context;
        }


        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

         
        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            CheckDisposed();
            return Context.SaveChangesAsync(cancellationToken);
        }

        public virtual DbConnection Connection
        {
            get
            {
                var connection = Context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                return connection;
            }
        }

        public DbTransaction Transaction => GetActiveTransaction();

        #region UnitofWork Transactions
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            CheckDisposed();
            var trans = Connection.BeginTransaction();
            Context.Database.UseTransaction(trans);
        }


        public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            CheckDisposed();
            var trans = await Connection.BeginTransactionAsync();
            Context.Database.UseTransaction(trans);

        }


        public void Rollback()
        {
            CheckDisposed();
            Transaction.Rollback();
        }

        public void Commit()
        {
            Context.Database.CurrentTransaction.Commit();
        }

        public async Task CommitAsync()
        {

            await Transaction.CommitAsync();
        }


        #endregion


        public virtual void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing && Context != null)
            {
                var currentTransaction = Context.Database.CurrentTransaction;
                if (Context.Database.GetDbConnection().State == ConnectionState.Open)
                {
                  
                    currentTransaction?.Dispose();
                    Connection.Close();
                }
                else
                {
                    currentTransaction?.Dispose();
                    Context.Dispose();

                }
            }
            _isDisposed = true;
        }

        protected void CheckDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException("The UnitOfWork is already disposed and cannot be used anymore.");
        }
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize((object)this);

        }

        private DbTransaction GetActiveTransaction()
        {
            return Context.Database.CurrentTransaction?.GetDbTransaction();
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }



    }
}
