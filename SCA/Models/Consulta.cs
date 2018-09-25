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
    public class Consulta
    {
        #region Atributos
        private int _idConsulta;
        private Pacientes _pacientes;
        private Usuarios _usuarios;
        private string _titulo;
        private string _sintoma;
        private DateTime? _dataConsulta;
        private bool _finalizar;

        #endregion

        #region Propriedades

        public int IdConsulta
        {
            get { return _idConsulta; }
            set { _idConsulta = value; }
        }
        public Pacientes Pacientes
        {
            get { return _pacientes; }
            set { _pacientes = value; }
        }
        public Usuarios Usuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }        
        public string Sintoma
        {
            get { return _sintoma; }
            set { _sintoma = value; }
        }
        public DateTime? DataConsulta
        {
            get { return _dataConsulta; }
            set { _dataConsulta = value; }
        }
        public bool Finalizar
        {
            get { return _finalizar; }
            set { _finalizar = value; }
        }
        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public Consulta()
        {
            Persisted = false;
        }

        public Consulta(int IdConsulta)
        {
            this._idConsulta = IdConsulta;
            Persisted = true;
        }
        #endregion
    }
}
