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
    public class ConsultaBll : BLLBase<Consulta>
    {
        public void Save(Consulta o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ConsultaDal(dal);

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

        public void Delete(Consulta o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ConsultaDal(dal);

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

        public Consulta GetObject(int idConsulta)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ConsultaDal(dal);

                try
                {
                    dal.OpenConnection();
                    var modelo = dao.GetObject(idConsulta);
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

        public List<Consulta> GetList()
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ConsultaDal(dal);

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


        public List<Consulta> GetList(string conditions = null, bool completeRelatedObjects = false)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ConsultaDal(dal);

                try
                {
                    var list = dao.GetList(conditions).OrderBy(o => o.IdConsulta).ToList();
                    foreach (Consulta p in list)
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


        public override void CompleteObject(Consulta o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("consulta");

            var dao = new ConsultaDal(dal);
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

        protected override void CompleteRelatedObjects(Consulta o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;

            if (o.Pacientes != null)
            {
                PacientesBll bll = new PacientesBll();
                bll.CompleteObject(o.Pacientes, dal, false);
            }
            if (o.Usuarios != null)
            {
                UsuariosBll bll = new UsuariosBll();
                bll.CompleteObject(o.Usuarios, dal, false);
            }

        }

    }
}
