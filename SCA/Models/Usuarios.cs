using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SCA.Models.ENUMS;

namespace SCA.Model
{
    [Serializable]
    public class Usuarios
    {
        #region Atributos
        private int _idUsuario;
        private Usuariosperfis _idPerfil;
        private string _crm;
        private Usuarioslicencas _idUsuarioLicenca;
        private string _nomeUsuario;
        private string _login;
        private string _senha;
        private string _email;
        private string _cpf;
        private string _rg;
        private string _localEmissao;
        private DateTime? _dataNascimento;
        private string _telefone1;
        private string _telefone2;
        private string _cep;
        private string _rua;
        private int _numero;
        private string _bairro;
        private string _cidade;
        private string _estado;
        private string _complemento;
        private StatusUsuarioLicenca _status;
        private bool _excluido;
        private Licencas _licenca;
        #endregion

        #region Propriedades

        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        public Usuariosperfis Perfil
        {
            get { return _idPerfil; }
            set { _idPerfil = value; }
        }
        public string Crm
        {
            get { return _crm; }
            set { _crm = value; }
        }
        public Usuarioslicencas IdUsuarioLicenca
        {
            get { return _idUsuarioLicenca; }
            set { _idUsuarioLicenca = value; }
        }

        public string NomeUsuario
        {
            get { return _nomeUsuario; }
            set { _nomeUsuario = value; }
        }
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string CPF
        {
            get { return _cpf; }
            set { _cpf = value; }
        }
        public string Rg
        {
            get { return _rg; }
            set { _rg = value; }
        }
        public string LocalEmissao
        {
            get { return _localEmissao; }
            set { _localEmissao = value; }
        }
        public DateTime? DataNascimento
        {
            get { return _dataNascimento; }
            set { _dataNascimento = value; }
        }
        public string Telefone1
        {
            get { return _telefone1; }
            set { _telefone1 = value; }
        }
        public string Telefone2
        {
            get { return _telefone2; }
            set { _telefone2 = value; }
        }
        public string Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }
        public string Rua
        {
            get { return _rua; }
            set { _rua = value; }
        }
        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }
        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }
        public StatusUsuarioLicenca Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public bool Excluido
        {
            get { return _excluido; }
            set { _excluido = value; }
        }
        public Licencas Licencas
        {
            get { return _licenca; }
            set { _licenca = value; }
        }
        public string GetMD5Hash()
        {
            string password = _senha;
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);

            byte[] hash = md5.ComputeHash(inputBytes);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();

        }


        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public Usuarios()
        {
            Persisted = false;
        }

        public Usuarios(int IdUsuario)
        {
            this._idUsuario = IdUsuario;
            Persisted = true;
        }
        #endregion
    }
}
