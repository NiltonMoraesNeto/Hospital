using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SCA.Model;
using SCA.DAL;
using SCA.Models.ENUMS;
using InterfaceConexao.DAL;
using MySql.Data.MySqlClient;

namespace SCA.Dal
{
    public class UsuariosDal : DALBase<Usuarios>
    {
        protected DataAccessLayer DAL;

        public UsuariosDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(Usuarios o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdUsuario", o.IdUsuario));
            parms.Add(new MySqlParameter("@IdPerfil", o.Perfil.IdPerfil));
            parms.Add(new MySqlParameter("@Crm", !String.IsNullOrEmpty(o.Crm) ? o.Crm : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@IdLicenca", o.Licencas.IdLicenca));
            parms.Add(new MySqlParameter("@NomeUsuario", !String.IsNullOrEmpty(o.NomeUsuario) ? o.NomeUsuario : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Login", !String.IsNullOrEmpty(o.Login) ? o.Login : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Senha", !String.IsNullOrEmpty(o.Senha) ? o.Senha : (object)DBNull.Value));
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
            parms.Add(new MySqlParameter("@Status", o.Status));
            parms.Add(new MySqlParameter("@Excluido", o.Excluido));

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
        public void Insert(Usuarios o)
        {
            String sql = "INSERT INTO Usuarios (IdPerfil, Crm, IdLicenca, NomeUsuario, Login, Senha, Email, CPF, Rg, " + 
                         "LocalEmissao, DataNascimento, Telefone1, Telefone2, Cep, Rua, Numero, Bairro, Cidade, Estado, " +
                         "Complemento, Status, Excluido)" +
                         " VALUES (@IdPerfil, @Crm, @IdLicenca, @NomeUsuario, @Login, @Senha, @Email, @CPF, @Rg, " + 
                         "@LocalEmissao, @DataNascimento,  @Telefone1, @Telefone2, @Cep, @Rua, @Numero, @Bairro, @Cidade, @Estado, " +
                         "@Complemento, @Status, @Excluido);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdUsuario = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;
        }
        public void Update(Usuarios o)
        {
            String sql = "UPDATE Usuarios SET IdPerfil = @IdPerfil, Crm = @Crm, IdLicenca = @IdLicenca, NomeUsuario = @NomeUsuario, " +
                         "Login = @Login, Senha = @Senha, Email = @Email, CPF = @CPF, Rg = @Rg, LocalEmissao = @LocalEmissao, " +
                         "DataNascimento = @DataNascimento, Telefone1 = @Telefone1, Telefone2 = @Telefone2, " +
                         "Cep = @Cep, Rua = @Rua, Numero = @Numero, Bairro = @Bairro, Cidade = @Cidade, " +
                         "Estado = @Estado, Complemento = @Complemento, Status = @Status, Excluido = @Excluido" +
                         " WHERE IdUsuario = @IdUsuario ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }

        public void UpdatePassword(Usuarios o)
        {
            String sql = "UPDATE Usuarios SET Senha = @Senha " +
                         "WHERE IdUsuario = @IdUsuario ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }

        //public void Delete(Usuarios o)
        //{
        //    //String sql = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario ";
        //    //var parms = new MySqlParameter("@IdUsuario", o.IdUsuario);
        //    //DAL.ExecuteNonQuery(sql, CommandType.Text, parms);


        //    //String sql = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario ";
        //    //var parms = (new MySqlParameter("@IdUsuario", o.IdUsuario));
        //    String sql = "UPDATE Usuarios SET  Excluido = @Excluido, Status = @Status WHERE IdUsuario = @IdUsuario ";

        //    var parms = new List<MySqlParameter>();

        //    parms.Add(new MySqlParameter("@IdUsuario", o.IdUsuario));
        //    parms.Add(new MySqlParameter("@Excluido", o.Excluido));
        //    parms.Add(new MySqlParameter("@Status", o.Status));

        //    DAL.ExecuteNonQuery(sql, CommandType.Text, parms);

        //}




        protected override void LoadObjectInternal(IDataReader dr, Usuarios o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
            o.Perfil = new Usuariosperfis(Convert.ToInt32(dr["IdPerfil"]));
            if (dr["Crm"] != DBNull.Value)
                o.Crm = Convert.ToString(dr["Crm"]);
            o.Licencas = new Licencas(Convert.ToInt32(dr["IdLicenca"]));
            if (dr["NomeUsuario"] != DBNull.Value)
                o.NomeUsuario = Convert.ToString(dr["NomeUsuario"]);
            if (dr["Login"] != DBNull.Value)
                o.Login = Convert.ToString(dr["Login"]);
            if (dr["Senha"] != DBNull.Value)
                o.Senha = Convert.ToString(dr["Senha"]);
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
            if (dr["Status"] != DBNull.Value)
                o.Status = (StatusUsuarioLicenca)Convert.ToInt32(dr["Status"]);
            o.Excluido = Convert.ToBoolean(dr["Excluido"]);
            o.Persisted = true;
        }
        public override void CompleteObject(Usuarios o)
        {
            using (var dr = GetObjectDataReader(o.IdUsuario))
            {
                LoadObject(dr, o);
            }
        }
        public void Login(Usuarios o)
        {
            using (var dr = GetLoginDataReader(o))
            {
                LoadObject(dr, o);
            }
        }

        private IDataReader GetLoginDataReader(Usuarios o)
        {
            //String sql = "SELECT * FROM Usuarios WHERE EmailNotificacao = @EmailNotificacao ";
            //String sql = "SELECT * FROM Usuarios WHERE CPF = @CPF ";
            String sql = "SELECT * FROM Usuarios WHERE Login = @Login ";

            var parms = new MySqlParameter("@Login", o.Login);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        private IDataReader GetObjectDataReader(int idUsuario)
        {
            String sql = "SELECT * FROM Usuarios WHERE IdUsuario = @IdUsuario ";

            var parms = new MySqlParameter("@IdUsuario", idUsuario);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Usuarios GetObject(int idUsuario)
        {
            using (var dr = GetObjectDataReader(idUsuario))
            {
                return ConvertToObject(dr);
            }
        }

        private IDataReader GetObjectDataReader(string login)
        {
            //String sql = "SELECT * FROM Usuarios WHERE EmailNotificacao = @EmailNotificacao ";
            //String sql = "SELECT * FROM Usuarios WHERE CPF = @CPF ";
            String sql = "SELECT * FROM Usuarios WHERE Login = @Login ";

            var parms = new MySqlParameter("@Login", login);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Usuarios GetObject(string login)
        {
            using (var dr = GetObjectDataReader(login))
            {
                return ConvertToObject(dr);
            }
        }

        //private IDataReader GetListDataReader(string conditions)
        //{


        //    String sql = "SELECT * FROM " +
        //                "FROM `clarodb_fator`.`usuarios` u " +
        //                "INNER JOIN `clarodb_fator`.`usuarios` uSup ON  uSup.`IdUsuario` = u.`IdUsuario`" +
        //                "WHERE u.`IdUsuario` = u.`IdUsuario`" +
        //                conditions;

        //    return DAL.ExecuteDataReader(sql, CommandType.Text);
        //}


        private IDataReader GetListDataReader(string conditions = null)
        {
            String sql = "SELECT * FROM Usuarios " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<Usuarios> GetList(string conditions = null)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
