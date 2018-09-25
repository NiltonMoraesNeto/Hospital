using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using SCA.Models.ENUMS;

namespace SCA.Model
{
    [Serializable]
    public class Pacientes
    {
        #region Atributos
        private int _idPaciente;        
        private Licencas _licenca;
        private PlanoSaude _planoSaude;
        private Usuarios _usuarios;
        private string _nomePaciente;
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
        private string _tipoSanguineo;
        private string _profissao;
        private bool _alergia;
        private string _obs;
        #endregion

        #region Propriedades

        public int IdPaciente
        {
            get { return _idPaciente; }
            set { _idPaciente = value; }
        }
        public Licencas Licencas
        {
            get { return _licenca; }
            set { _licenca = value; }
        }
        public PlanoSaude PlanoSaude
        {
            get { return _planoSaude; }
            set { _planoSaude = value; }
        }
        public Usuarios Usuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }
        public string NomePaciente
        {
            get { return _nomePaciente; }
            set { _nomePaciente = value; }
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
        public string TipoSanguineo
        {
            get { return _tipoSanguineo; }
            set { _tipoSanguineo = value; }
        }
        public string Profissao
        {
            get { return _profissao; }
            set { _profissao = value; }
        }
        public bool Alergia
        {
            get { return _alergia; }
            set { _alergia = value; }
        }
        public string Obs
        {
            get { return _obs; }
            set { _obs = value; }
        }
        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public Pacientes()
        {
            Persisted = false;
        }

        public Pacientes(int IdPaciente)
        {
            this._idPaciente = IdPaciente;
            Persisted = true;
        }
        #endregion
    }
}
