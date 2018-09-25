using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SCA.Models;
using SCA.DAL;
using InterfaceConexao.DAL;
using MySql.Data.MySqlClient;
using SCA.Model;

namespace SCA.Dal
{
    public class ModeloDal : DALBase<Modelo>
    {
        protected DataAccessLayer DAL;

        public ModeloDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(Modelo o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdModelo", o.IdModelo));
            parms.Add(new MySqlParameter("@Descricao", !String.IsNullOrEmpty(o.Descricao) ? o.Descricao : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@IdLicenca", o.Licencas.IdLicenca));
            if (!o.Persisted)
            {
                parms[0].Direction = ParameterDirection.Output;
            }
            else
            {
                parms[0].Direction = ParameterDirection.Input;
            }
            return parms;

        }
        public void Insert(Modelo o)
        {
            String sql = "INSERT INTO Modelo (Descricao, IdLicenca)" +
                         " VALUES (@Descricao, @IdLicenca);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdModelo = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;

        }
        public void Update(Modelo o)
        {
            String sql = "UPDATE Modelo SET Descricao = @Descricao, IdLicenca = @IdLicenca WHERE IdModelo = @IdModelo ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        public void Delete(Modelo o)
        {
            String sql = "DELETE FROM Modelo WHERE IdModelo = @IdModelo ";
            DAL.ExecuteNonQuery(sql, CommandType.Text, new MySqlParameter("@IdModelo", o.IdModelo));
            o.Persisted = false;
        }

        protected override void LoadObjectInternal(IDataReader dr, Modelo o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdModelo = Convert.ToInt32(dr["IdModelo"]);
            if (dr["Descricao"] != DBNull.Value)
                o.Descricao = Convert.ToString(dr["Descricao"]);
            o.Licencas = new Licencas(Convert.ToInt32(dr["IdLicenca"]));
            o.Persisted = true;
        }
        public override void CompleteObject(Modelo o)
        {
            using (var dr = GetObjectDataReader(o.IdModelo))
            {
                LoadObject(dr, o);
            }
        }
        private IDataReader GetObjectDataReader(int idTeste)
        {
            String sql = "SELECT * FROM Modelo WHERE IdModelo = @IdModelo ";

            var parms = new MySqlParameter("@IdModelo", idTeste);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Modelo GetObject(int idTeste)
        {
            using (var dr = GetObjectDataReader(idTeste))
            {
                return ConvertToObject(dr);
            }
        }
        private IDataReader GetListDataReader()
        {
            String sql = "SELECT * FROM Modelo ";

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }

        private IDataReader GetListDataReader(string conditions)
        {
            String sql = "SELECT * FROM Modelo " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<Modelo> GetList()
        {
            using (var dr = GetListDataReader())
            {
                return ConvertToList(dr);
            }
        }
        public List<Modelo> GetList(string conditions)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
