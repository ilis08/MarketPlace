using Data.Context;
using Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class UnitOfWork : IDisposable
    {
        private StoreDBContext context = new StoreDBContext();

        private GenericRepository<Phone> phoneRepository;
        private GenericRepository<Category> categoryRepository;

        public GenericRepository<Phone> PhoneRepository
        {
            get
            {
                if (this.phoneRepository == null)
                {
                    this.phoneRepository = new GenericRepository<Phone>(context);
                }
                return phoneRepository;
            }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }
                return categoryRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        //Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
