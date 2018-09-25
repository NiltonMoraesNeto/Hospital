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
    public class PlanoSaudeDal : DALBase<PlanoSaude>
    {
        protected DataAccessLayer DAL;

        public PlanoSaudeDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(PlanoSaude o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdPlanoSaude", o.IdPlanoSaude));
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
        public void Insert(PlanoSaude o)
        {
            String sql = "INSERT INTO PlanoSaude (Descricao, IdLicenca)" +
                         " VALUES (@Descricao, @IdLicenca);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdPlanoSaude = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;

        }
        public void Update(PlanoSaude o)
        {
            String sql = "UPDATE PlanoSaude SET Descricao = @Descricao, IdLicenca = @IdLicenca WHERE IdPlanoSaude = @IdPlanoSaude ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        public void Delete(PlanoSaude o)
        {
            String sql = "DELETE FROM PlanoSaude WHERE IdPlanoSaude = @IdPlanoSaude ";
            DAL.ExecuteNonQuery(sql, CommandType.Text, new MySqlParameter("@IdPlanoSaude", o.IdPlanoSaude));
            o.Persisted = false;
        }

        protected override void LoadObjectInternal(IDataReader dr, PlanoSaude o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdPlanoSaude = Convert.ToInt32(dr["IdPlanoSaude"]);
            if (dr["Descricao"] != DBNull.Value)
                o.Descricao = Convert.ToString(dr["Descricao"]);
            o.Licencas = new Licencas(Convert.ToInt32(dr["IdLicenca"]));
            o.Persisted = true;
        }
        public override void CompleteObject(PlanoSaude o)
        {
            using (var dr = GetObjectDataReader(o.IdPlanoSaude))
            {
                LoadObject(dr, o);
            }
        }
        private IDataReader GetObjectDataReader(int idPlanoSaude)
        {
            String sql = "SELECT * FROM PlanoSaude WHERE IdPlanoSaude = @IdPlanoSaude ";

            var parms = new MySqlParameter("@IdPlanoSaude", idPlanoSaude);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public PlanoSaude GetObject(int idPlanoSaude)
        {
            using (var dr = GetObjectDataReader(idPlanoSaude))
            {
                return ConvertToObject(dr);
            }
        }
        private IDataReader GetListDataReader()
        {
            String sql = "SELECT * FROM PlanoSaude ";

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }

        private IDataReader GetListDataReader(string conditions)
        {
            String sql = "SELECT * FROM PlanoSaude " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<PlanoSaude> GetList()
        {
            using (var dr = GetListDataReader())
            {
                return ConvertToList(dr);
            }
        }
        public List<PlanoSaude> GetList(string conditions)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
