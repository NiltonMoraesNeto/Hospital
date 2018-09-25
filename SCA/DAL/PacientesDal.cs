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
    public class PacientesDal : DALBase<Pacientes>
    {
        protected DataAccessLayer DAL;

        public PacientesDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(Pacientes o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdPaciente", o.IdPaciente));
            parms.Add(new MySqlParameter("@IdLicenca", o.Licencas.IdLicenca));
            parms.Add(new MySqlParameter("@IdPlanoSaude", o.PlanoSaude.IdPlanoSaude));
            parms.Add(new MySqlParameter("@IdUsuario", o.Usuarios.IdUsuario));
            parms.Add(new MySqlParameter("@NomePaciente", !String.IsNullOrEmpty(o.NomePaciente) ? o.NomePaciente : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Email", !String.IsNullOrEmpty(o.Email) ? o.Email : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@CPF", !String.IsNullOrEmpty(o.CPF) ? o.CPF : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Rg", !String.IsNullOrEmpty(o.Rg) ? o.Rg : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@LocalEmissao", !String.IsNullOrEmpty(o.LocalEmissao) ? o.LocalEmissao : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@DataNascimento", (o.DataNascimento.HasValue) ? o.DataNascimento : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Telefone1", !String.IsNullOrEmpty(o.Telefone1) ? o.Telefone1 : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Telefone2", !String.IsNullOrEmpty(o.Telefone2) ? o.Telefone2 : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Cep", !String.IsNullOrEmpty(o.Cep) ? o.Cep : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Rua", !String.IsNullOrEmpty(o.Rua) ? o.Rua : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Numero", o.Numero));
            parms.Add(new MySqlParameter("@Bairro", !String.IsNullOrEmpty(o.Bairro) ? o.Bairro : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Cidade", !String.IsNullOrEmpty(o.Cidade) ? o.Cidade : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Estado", !String.IsNullOrEmpty(o.Estado) ? o.Estado : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Complemento", !String.IsNullOrEmpty(o.Complemento) ? o.Complemento : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@TipoSanguineo", !String.IsNullOrEmpty(o.TipoSanguineo) ? o.TipoSanguineo : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Profissao", !String.IsNullOrEmpty(o.Profissao) ? o.Profissao : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Alergia", o.Alergia));
            parms.Add(new MySqlParameter("@Obs", !String.IsNullOrEmpty(o.Obs) ? o.Obs : (object)DBNull.Value));

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
        public void Insert(Pacientes o)
        {
            String sql = "INSERT INTO Pacientes (IdLicenca, IdPlanoSaude, IdUsuario, NomePaciente, Email, CPF, Rg, " +
                         "LocalEmissao, DataNascimento, Telefone1, Telefone2, Cep, Rua, Numero, Bairro, Cidade, Estado, " +
                         "Complemento, TipoSanguineo, Profissao, Alergia, Obs)" +
                         " VALUES (@IdLicenca, @IdPlanoSaude, @IdUsuario, @NomePaciente, @Email, @CPF, @Rg, " +
                         "@LocalEmissao, @DataNascimento,  @Telefone1, @Telefone2, @Cep, @Rua, @Numero, @Bairro, @Cidade, @Estado, " +
                         "@Complemento, @TipoSanguineo, @Profissao, @Alergia, @Obs);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdPaciente = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;

        }
        public void Update(Pacientes o)
        {
            String sql = "UPDATE Pacientes SET IdLicenca = @IdLicenca, IdPlanoSaude = @IdPlanoSaude, " +
                         "IdUsuario = @IdUsuario, NomePaciente = @NomePaciente, Email = @Email, CPF = @CPF, Rg = @Rg, LocalEmissao = @LocalEmissao, " +
                         "DataNascimento = @DataNascimento, Telefone1 = @Telefone1, Telefone2 = @Telefone2, " +
                         "Cep = @Cep, Rua = @Rua, Numero = @Numero, Bairro = @Bairro, Cidade = @Cidade, " +
                         "Estado = @Estado, Complemento = @Complemento, TipoSanguineo = @TipoSanguineo, Profissao = @Profissao, " +
                         "Alergia = @Alergia, Obs = @Obs" +
                         " WHERE IdPaciente = @IdPaciente ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        public void Delete(Pacientes o)
        {
            String sql = "DELETE FROM Pacientes WHERE IdPaciente = @IdPaciente ";
            DAL.ExecuteNonQuery(sql, CommandType.Text, new MySqlParameter("@IdPaciente", o.IdPaciente));
            o.Persisted = false;
        }

        protected override void LoadObjectInternal(IDataReader dr, Pacientes o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdPaciente = Convert.ToInt32(dr["IdPaciente"]);
            o.Licencas = new Licencas(Convert.ToInt32(dr["IdLicenca"]));
            o.PlanoSaude = new PlanoSaude(Convert.ToInt32(dr["IdPlanoSaude"]));
            o.Usuarios = new Usuarios(Convert.ToInt32(dr["IdUsuario"]));
            if (dr["NomePaciente"] != DBNull.Value)
                o.NomePaciente = Convert.ToString(dr["NomePaciente"]);
            if (dr["Email"] != DBNull.Value)
                o.Email = Convert.ToString(dr["Email"]);
            if (dr["CPF"] != DBNull.Value)
                o.CPF = Convert.ToString(dr["CPF"]);
            if (dr["Rg"] != DBNull.Value)
                o.Rg = Convert.ToString(dr["Rg"]);
            if (dr["LocalEmissao"] != DBNull.Value)
                o.LocalEmissao = Convert.ToString(dr["LocalEmissao"]);
            if (dr["DataNascimento"] != DBNull.Value)
                o.DataNascimento = Convert.ToDateTime(dr["DataNascimento"]);
            if (dr["Telefone1"] != DBNull.Value)
                o.Telefone1 = Convert.ToString(dr["Telefone1"]);
            if (dr["Telefone2"] != DBNull.Value)
                o.Telefone2 = Convert.ToString(dr["Telefone2"]);
            if (dr["Cep"] != DBNull.Value)
                o.Cep = Convert.ToString(dr["Cep"]);
            if (dr["Rua"] != DBNull.Value)
                o.Rua = Convert.ToString(dr["Rua"]);
            o.Numero = Convert.ToInt32(dr["Numero"]);
            if (dr["Bairro"] != DBNull.Value)
                o.Bairro = Convert.ToString(dr["Bairro"]);
            if (dr["Cidade"] != DBNull.Value)
                o.Cidade = Convert.ToString(dr["Cidade"]);
            if (dr["Estado"] != DBNull.Value)
                o.Estado = Convert.ToString(dr["Estado"]);
            if (dr["Complemento"] != DBNull.Value)
                o.Complemento = Convert.ToString(dr["Complemento"]);
            if (dr["TipoSanguineo"] != DBNull.Value)
                o.TipoSanguineo = Convert.ToString(dr["TipoSanguineo"]);
            if (dr["Profissao"] != DBNull.Value)
                o.Profissao = Convert.ToString(dr["Profissao"]);
            o.Alergia = Convert.ToBoolean(dr["Alergia"]);
            if (dr["Obs"] != DBNull.Value)
                o.Obs = Convert.ToString(dr["Obs"]);
            o.Persisted = true;
        }
        public override void CompleteObject(Pacientes o)
        {
            using (var dr = GetObjectDataReader(o.IdPaciente))
            {
                LoadObject(dr, o);
            }
        }
        private IDataReader GetObjectDataReader(int idPacientes)
        {
            String sql = "SELECT * FROM Pacientes WHERE IdPaciente = @IdPaciente ";

            var parms = new MySqlParameter("@IdPaciente", idPacientes);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Pacientes GetObject(int idPacientes)
        {
            using (var dr = GetObjectDataReader(idPacientes))
            {
                return ConvertToObject(dr);
            }
        }
        private IDataReader GetListDataReader()
        {
            String sql = "SELECT * FROM Pacientes ";

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }

        private IDataReader GetListDataReader(string conditions)
        {
            String sql = "SELECT * FROM Pacientes " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<Pacientes> GetList()
        {
            using (var dr = GetListDataReader())
            {
                return ConvertToList(dr);
            }
        }
        public List<Pacientes> GetList(string conditions)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
