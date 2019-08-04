using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Babaganoush.Core.Data.Interfaces
{
    /// <summary>
    /// Interface for repository.
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Gets all objects from database http://blog.falafel.com/blogs/basem-emara/2013/02/05/generic-
        /// repository-pattern-with-entity-framework-and-web-api.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        ///
        /// <returns>
        /// An IQueryable&lt;T&gt;
        /// </returns>
        IQueryable<T> All<T>() where T : class;

        /// <summary>
        /// Selects a single item by specified expression.
        /// </summary>
        ///
        /// <typeparam name="T">.</typeparam>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="expression">.</param>
        ///
        /// <returns>
        /// A T.
        /// </returns>
        T Get<T>(Expression<Func<T, bool>> expression) where T : class;

        /// <summary>
        /// Finds a single item by specified expression.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="predicate">.</param>
        ///
        /// <returns>
        /// A T.
        /// </returns>
        T Find<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="predicate">Specified a filter.</param>
        ///
        /// <returns>
        /// An IQueryable&lt;T&gt;
        /// </returns>
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets objects from database with filtering and paging.
        /// </summary>
        ///
        /// <typeparam name="T">.</typeparam>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="filter">Specified a filter.</param>
        /// <param name="total">[out] Returns the total records count of the filter.</param>
        /// <param name="index">(Optional) Specified the page index.</param>
        /// <param name="size">(Optional) Specified the page size.</param>
        ///
        /// <returns>
        /// An IQueryable&lt;T&gt;
        /// </returns>
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
            where T : class;

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="predicate">Specified the filter expression.</param>
        ///
        /// <returns>
        /// true if the object is in this collection, false if not.
        /// </returns>
        bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Creates a new object to database.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="value">Specified a new object to create.</param>
        ///
        /// <returns>
        /// A T.
        /// </returns>
        T Create<T>(T value) where T : class;

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="value">Specified a existing object to delete.</param>
        ///
        /// <returns>
        /// An int.
        /// </returns>
        int Delete<T>(T value) where T : class;

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="predicate">.</param>
        ///
        /// <returns>
        /// An int.
        /// </returns>
        int Delete<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="value">Specified the object to save.</param>
        ///
        /// <returns>
        /// An int.
        /// </returns>
        int Update<T>(T value) where T : class;

        /// <summary>
        /// Saves the changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Executes the procedure.
        /// </summary>
        ///
        /// <param name="procedureCommand">The procedure command.</param>
        /// <param name="sqlParams">The SQL params.</param>
        void ExecuteProcedure(string procedureCommand, params SqlParameter[] sqlParams);
    }
}
