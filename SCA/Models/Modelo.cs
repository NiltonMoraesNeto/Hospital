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
    public class Modelo
    {
        #region Atributos
        private int _idModelo;
        private string _descricao;
        private Licencas _licenca;
        #endregion

        #region Propriedades

        public int IdModelo
        {
            get { return _idModelo; }
            set { _idModelo = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public Licencas Licencas
        {
            get { return _licenca; }
            set { _licenca = value; }
        }
        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public Modelo()
        {
            Persisted = false;
        }

        public Modelo(int IdModelo)
        {
            this._idModelo = IdModelo;
            Persisted = true;
        }
        #endregion
    }
}
