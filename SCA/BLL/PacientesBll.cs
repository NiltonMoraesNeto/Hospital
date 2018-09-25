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
    public class PacientesBll : BLLBase<Pacientes>
    {
        public void Save(Pacientes o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PacientesDal(dal);

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

        public void Delete(Pacientes o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PacientesDal(dal);

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

        public Pacientes GetObject(int idPacientes)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PacientesDal(dal);

                try
                {
                    dal.OpenConnection();
                    var modelo = dao.GetObject(idPacientes);
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

        public List<Pacientes> GetList()
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PacientesDal(dal);

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


        public List<Pacientes> GetList(string conditions = null, bool completeRelatedObjects = false)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new PacientesDal(dal);

                try
                {
                    var list = dao.GetList(conditions).OrderBy(o => o.NomePaciente).ToList();
                    foreach (Pacientes p in list)
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


        public override void CompleteObject(Pacientes o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("pacientes");

            var dao = new PacientesDal(dal);
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

        protected override void CompleteRelatedObjects(Pacientes o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;

            if (o.Licencas != null)
            {
                LicencasBll bll = new LicencasBll();
                bll.CompleteObject(o.Licencas, dal, false);
            }
            if (o.PlanoSaude != null)
            {
                PlanoSaudeBll bll = new PlanoSaudeBll();
                bll.CompleteObject(o.PlanoSaude, dal, false);
            }
            if (o.Usuarios != null)
            {
                UsuariosBll bll = new UsuariosBll();
                bll.CompleteObject(o.Usuarios, dal, false);
            }

        }

    }
}
