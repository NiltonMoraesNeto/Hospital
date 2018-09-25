using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCA.BLL;
using SCA.Dal;
using SCA.Models;
using SCA.Model;
using InterfaceConexao.DAL;

namespace SCA.Bll
{
    public class PlanoSaudeBll : BLLBase<PlanoSaude>
    {
        public void Save(PlanoSaude o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PlanoSaudeDal(dal);

                try
                {
                    if (!o.Persisted)
                    {
                        dao.Insert(o);
                    }
                    else
                    {
                        dao.Update(o);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Delete(PlanoSaude o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PlanoSaudeDal(dal);

                try
                {
                    dao.Delete(o);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public PlanoSaude GetObject(int idPlanoSaude)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PlanoSaudeDal(dal);

                try
                {
                    dal.OpenConnection();
                    var modelo = dao.GetObject(idPlanoSaude);
                    CompleteRelatedObjects(modelo, dal);
                    return modelo;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dal.CloseConnection();
                }
            }
        }

        public List<PlanoSaude> GetList()
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PlanoSaudeDal(dal);

                try
                {
                    return dao.GetList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<PlanoSaude> GetList(string conditions = null, bool completeRelatedObjects = false)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PlanoSaudeDal(dal);

                try
                {
                    var list = dao.GetList(conditions).OrderBy(o => o.Descricao).ToList();
                    foreach (PlanoSaude p in list)
                    {
                        CompleteRelatedObjects(p, dal);
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public override void CompleteObject(PlanoSaude o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("modelo");

            var dao = new PlanoSaudeDal(dal);
            var connOpened = dal.ConnectionOpened;

            try
            {
                dal.OpenConnection(!connOpened);
                dao.CompleteObject(o);
                CompleteRelatedObjects(o, dal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dal.CloseConnection(!connOpened);
            }
        }

        protected override void CompleteRelatedObjects(PlanoSaude o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;

            if (o.Licencas != null)
            {
                LicencasBll bll = new LicencasBll();
                bll.CompleteObject(o.Licencas, dal, false);
            }

        }

    }
}
