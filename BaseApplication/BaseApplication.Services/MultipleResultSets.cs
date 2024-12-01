using BaseApplication.Repository.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace BaseApplication.Services
{
    public static class MultipleResultSets
    {
        public static MultipleResultSetWrapper MultipleResults(this BaseApplicationDbContext db, SqlCommand storedProcedure)
        {
            return new MultipleResultSetWrapper(db, storedProcedure);
        }

        public class MultipleResultSetWrapper
        {
            private readonly BaseApplicationDbContext _db;
            private readonly SqlCommand _storedProcedure;
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> _resultSets;

            public MultipleResultSetWrapper(BaseApplicationDbContext db, SqlCommand storedProcedure)
            {
                _db = db;
                _storedProcedure = storedProcedure;
                _resultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }

            public MultipleResultSetWrapper With<TResult>()
            {
                _resultSets.Add((adapter, reader) => adapter
                .ObjectContext
                .Translate<TResult>(reader)
                .ToList());

                return this;
            }

            //public async Task<List<IEnumerable>> Execute()
            //{
            //    var results = new List<IEnumerable>();
            //    try
            //    {

            //        using (var connection = _db.Database.GetDbConnection())
            //        {
            //            connection.Open();
            //            _storedProcedure.Connection = (SqlConnection)connection;
            //            //using (var reader = await _storedProcedure.ExecuteReaderAsync())
            //            //{
            //            //    foreach(var row in reader)
            //            //    {
            //            //        results.Add((IDataRecord)row);
            //            //        reader.NextResultAsync();
            //            //    }
            //            //}
            //            using (var reader = await _storedProcedure.ExecuteReaderAsync())
            //            {
            //                var adapter = ((IObjectContextAdapter)_db);
            //                foreach (var resultSet in _resultSets)
            //                {
            //                    results.Add(resultSet(adapter, reader));
            //                    reader.NextResult();
            //                }
            //            }

            //            return results;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //    finally
            //    {
            //        _db.Database.CloseConnection();
            //        _db.Database.GetDbConnection().Dispose();
            //    }

            public async Task<List<IEnumerable>> Execute()
            {
                var results = new List<IEnumerable>();
                try
                {

                    using (var connection = _db.Database.GetDbConnection())
                    {
                        connection.Open();
                        _storedProcedure.Connection = (SqlConnection)connection;
                        //using (var reader = await _storedProcedure.ExecuteReaderAsync())
                        //{
                        //    foreach(var row in reader)
                        //    {
                        //        results.Add((IDataRecord)row);
                        //        reader.NextResultAsync();
                        //    }
                        //}
                        using (var reader = await _storedProcedure.ExecuteReaderAsync())
                        {
                            var adapter = ((IObjectContextAdapter)_db);
                            foreach (var resultSet in _resultSets)
                            {
                                results.Add(resultSet(adapter, reader));
                                reader.NextResult();
                            }
                        }

                        return results;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _db.Database.CloseConnection();
                    _db.Database.GetDbConnection().Dispose();
                }

                static void ReadSingleRow(IDataRecord dataRecord)
                {
                    Console.WriteLine(String.Format("{0}", dataRecord[0]));
                }
            }
        }
    }
}
