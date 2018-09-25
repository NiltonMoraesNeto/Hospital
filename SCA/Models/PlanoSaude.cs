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
    public class PlanoSaude
    {
        #region Atributos
        private int _idPlanoSaude;
        private string _descricao;
        private Licencas _licenca;
        #endregion

        #region Propriedades

        public int IdPlanoSaude
        {
            get { return _idPlanoSaude; }
            set { _idPlanoSaude = value; }
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

        public PlanoSaude()
        {
            Persisted = false;
        }

        public PlanoSaude(int IdPlanoSaude)
        {
            this._idPlanoSaude = IdPlanoSaude;
            Persisted = true;
        }
        #endregion
    }
}
