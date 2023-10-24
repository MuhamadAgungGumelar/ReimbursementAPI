using ReimbursementAPI.Data;
using ReimbursementAPI.Utilities.Handler;

namespace ReimbursementAPI.Repository
{
    public class GeneralRepository<TEntity> where TEntity : class
    {
        protected readonly ReimbursementDBContext _context;

        public GeneralRepository(ReimbursementDBContext context)
        {
            _context = context;
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList(); // ORM melakukan get all
        }
        public TEntity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<TEntity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity; // ORM melakukan get by guid
        }
        public TEntity? Create(TEntity entity)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;

            }
            catch (Exception ex)
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message); //melemparkan error
            }
        }
        public bool Update(TEntity entity)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message); //melemparkan error
            }
        }
        public bool Delete(TEntity entity)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_nik"))
                {
                    throw new ExceptionHandler("NIK already exists");
                }
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
                {
                    throw new ExceptionHandler("Email already exists");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
                {
                    throw new ExceptionHandler("Phone number already exists");
                }
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message); //melemparkan error
            }
        }
    }
}
