using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace SLBenefits.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
    public class UnitOfWork : DisposableBase, IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var dbEntityValidationResult in e.EntityValidationErrors)
                {
                    foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        // very bad
                        Console.WriteLine(dbValidationError.ErrorMessage);
                    }
                }
            }
        }

        public static IUnitOfWork Begin()
        {
            return DependencyResolver.Current.GetService<IUnitOfWork>(); ;
        }

        protected override void OnDisposing(bool disposing)
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}