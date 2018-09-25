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
    public class ConsultaDal : DALBase<Consulta>
    {
        protected DataAccessLayer DAL;

        public ConsultaDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(Consulta o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdConsulta", o.IdConsulta));
            parms.Add(new MySqlParameter("@IdPaciente", o.Pacientes.IdPaciente));
            parms.Add(new MySqlParameter("@IdUsuario", o.Usuarios.IdUsuario));
            parms.Add(new MySqlParameter("@Titulo", !String.IsNullOrEmpty(o.Titulo) ? o.Titulo : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Sintoma", !String.IsNullOrEmpty(o.Sintoma) ? o.Sintoma : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@DataConsulta", (o.DataConsulta.HasValue) ? o.DataConsulta : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Finalizar", o.Finalizar));

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
        public void Insert(Consulta o)
        {
            String sql = "INSERT INTO Consulta (IdPaciente, IdUsuario, Titulo, Sintoma, DataConsulta, Finalizar)" +
                         " VALUES (@IdPaciente, @IdUsuario, @Titulo, @Sintoma, @DataConsulta, @Finalizar);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdConsulta = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;

        }
        public void Update(Consulta o)
        {
            String sql = "UPDATE Consulta SET IdPaciente = @IdPaciente, IdUsuario = @IdUsuario, " +
                         "Titulo = @Titulo, Sintoma = @Sintoma, DataConsulta = @DataConsulta, Finalizar = @Finalizar" +
                         " WHERE IdConsulta = @IdConsulta ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        public void Delete(Consulta o)
        {
            String sql = "DELETE FROM Consulta WHERE IdConsulta = @IdConsulta ";
            DAL.ExecuteNonQuery(sql, CommandType.Text, new MySqlParameter("@IdConsulta", o.IdConsulta));
            o.Persisted = false;
        }

        protected override void LoadObjectInternal(IDataReader dr, Consulta o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdConsulta = Convert.ToInt32(dr["IdConsulta"]);
            o.Pacientes = new Pacientes(Convert.ToInt32(dr["IdPaciente"]));
            o.Usuarios = new Usuarios(Convert.ToInt32(dr["IdUsuario"]));
            if (dr["Titulo"] != DBNull.Value)
                o.Titulo = Convert.ToString(dr["Titulo"]);
            if (dr["Sintoma"] != DBNull.Value)
                o.Sintoma = Convert.ToString(dr["Sintoma"]);
            if (dr["DataConsulta"] != DBNull.Value)
                o.DataConsulta = Convert.ToDateTime(dr["DataConsulta"]);
            o.Finalizar = Convert.ToBoolean(dr["Finalizar"]);
            o.Persisted = true;
        }
        public override void CompleteObject(Consulta o)
        {
            using (var dr = GetObjectDataReader(o.IdConsulta))
            {
                LoadObject(dr, o);
            }
        }
        private IDataReader GetObjectDataReader(int idConsulta)
        {
            String sql = "SELECT * FROM Consulta WHERE IdConsulta = @IdConsulta ";

            var parms = new MySqlParameter("@IdConsulta", idConsulta);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Consulta GetObject(int idConsulta)
        {
            using (var dr = GetObjectDataReader(idConsulta))
            {
                return ConvertToObject(dr);
            }
        }
        private IDataReader GetListDataReader()
        {
            String sql = "SELECT * FROM Consulta ";

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }

        private IDataReader GetListDataReader(string conditions)
        {
            String sql = "SELECT * FROM Consulta " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<Consulta> GetList()
        {
            using (var dr = GetListDataReader())
            {
                return ConvertToList(dr);
            }
        }
        public List<Consulta> GetList(string conditions)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
