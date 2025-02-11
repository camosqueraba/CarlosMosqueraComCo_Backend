using DAL.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.DataContext;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class PublicacionRepository : IPublicacionRepository
    {
        private readonly ApplicationDBContext_SQLServer DBContext;
        public PublicacionRepository(ApplicationDBContext_SQLServer dbContext)
        {
            DBContext = dbContext;
        }
        /*
        public async Task<int> Create(Publicacion publicacion)
        {
            int idPublicacionCreated;
            Oper
            try
            {
                DBContext.Add(publicacion);
                await DBContext.SaveChangesAsync();
                idPublicacionCreated = publicacion.Id;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("PublicacionRepository.Create(Publicacion publicacion) Exception: ", ex.Message));

            }

            return idPublicacionCreated;
        }
        */
        public async Task<ResultadoOperacion<int>> Create(Publicacion publicacion)
        {            
            ResultadoOperacion<int> resultadoOperacionCreate = new();
            try
            {
                DBContext.Add(publicacion);
                int resultado = await DBContext.SaveChangesAsync();

                if (resultado > 0)
                {
                    resultadoOperacionCreate.OperacionCompletada = true;
                    resultadoOperacionCreate.DatosResultado = publicacion.Id;
                }
                else
                {
                    resultadoOperacionCreate.OperacionCompletada = false;
                    resultadoOperacionCreate.Origen = "PublicacionRepository.Create";
                    resultadoOperacionCreate.Error = "No se pudo guardar en DB";
                }
                /*
                resultadoOperacionCreate = new ResultadoOperacion<int>()
                {
                    OperacionCompletada = true,
                    DatosResultado = publicacion.Id,

                };
                */
            }
            catch (Exception ex)
            {
                resultadoOperacionCreate.OperacionCompletada = false;
                resultadoOperacionCreate.Origen = "PublicacionRepository.Create";
                resultadoOperacionCreate.Error = ex.Message;
            }

            return resultadoOperacionCreate;
        }
        
        public async Task<ResultadoOperacion<int>> Delete(int id)
        {
            ResultadoOperacion<int> resultadoOperacionDelete = new();
            try
            {
                Publicacion publicacion = await DBContext.Publicaciones.FirstAsync(c => c.Id == id);

                DBContext.Remove(publicacion);
                int resultado = await DBContext.SaveChangesAsync();

                if (resultado > 0)
                {
                    resultadoOperacionDelete.OperacionCompletada = true;
                    resultadoOperacionDelete.DatosResultado = 0;
                }
                else
                {
                    resultadoOperacionDelete.OperacionCompletada = false;
                    resultadoOperacionDelete.Origen = "PublicacionRepository.Delete";
                    resultadoOperacionDelete.Error = "No se pudo eliminar de DB";
                }
            }            
            catch (Exception ex)
            {
                resultadoOperacionDelete.OperacionCompletada = false;
                resultadoOperacionDelete.Origen = "PublicacionRepository.Delete";
                resultadoOperacionDelete.Error = ex.Message;
            }
            return resultadoOperacionDelete;
        }
        
        /*
        public async Task<int> Delete(int id)
        {
            int response;
            try
            {
                Publicacion publicacion = await DBContext.Publicaciones.FirstAsync(c => c.Id == id);

                DBContext.Remove(publicacion);
                response = await DBContext.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Delete() Exception: ", ex.Message));
            }
            return response;
        }
        */
        public async Task<List<Publicacion>> GetAll()
        {
            List<Publicacion> publicacions = null;

            try
            {
                publicacions = await DBContext.Publicaciones.ToListAsync();
                //var publicacionsTransform = publicacions.Select(p => new { Publicacion = p, ConteoComentarios = p.Comentarios.Count() });
            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("PublicacionRepository.GetAll(Publicacion publicacion) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("PublicacionRepository.GetAll(Publicacion publicacion) Exception: ", ex.Message));
            }


            return publicacions;
        }

        public async Task<ResultadoOperacion<Publicacion>> GetById(int id)
        {
            Publicacion publicacion = null;
            ResultadoOperacion<Publicacion> resultadoOperacion = new();
            try
            {
                publicacion = await DBContext.Publicaciones.AsNoTracking().Include("Comentarios").FirstOrDefaultAsync(publicacion => publicacion.Id == id);
                
                resultadoOperacion.DatosResultado = publicacion;
                resultadoOperacion.OperacionCompletada = true;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("GetById() Exception: ", ex.Message));
            }

            return resultadoOperacion;
        }


        public async Task<int> Update(Publicacion publicacion)
        {
            int response;
            try
            {

                DBContext.Update(publicacion);
                await DBContext.SaveChangesAsync();
                response = publicacion.Id;
                
            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("PublicacionRepository.Update(Publicacion publicacion) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("PublicacionRepository.Update(Publicacion publicacion) Exception: ", ex.Message));
            }

            return response;
        }


        public async Task<bool> ExistePublicacion(int id)
        {
            bool existePublicacion;
            try
            {
                bool result = await DBContext.Publicaciones.AsNoTracking().AnyAsync(x => x.Id == id);
                existePublicacion = result;
            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("PublicacionRepository.ExistePublicacion(int id) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("PublicacionRepository.ExistePublicacion(int id) Exception: ", ex.Message));
            }

            return existePublicacion;
        }

        //public async Task<List<Publicacion>> GetPublicacionsDetalle()
        //{
        //    var connectionString = _dbContext.Database.GetConnectionString();

        //    List<Publicacion> publicacionsDetalle = new();


        //    using (SqlConnection _SqlConnection = new(connectionString))
        //    {
        //        try
        //        {
        //            try
        //            {
        //                await _SqlConnection.OpenAsync();

        //                SqlCommand _SqlCommand = new("dbo.SPGetDetallePublicacions", _SqlConnection)
        //                {
        //                    CommandType = CommandType.StoredProcedure
        //                };

        //                //_SqlCommand.Parameters.AddWithValue("@id", id);

        //                using (SqlDataReader _SqlDataReader = await _SqlCommand.ExecuteReaderAsync())
        //                {
        //                    while (await _SqlDataReader.ReadAsync())
        //                    {
        //                        publicacionsDetalle.Add(new Publicacion()
        //                        {
        //                            //Id = _SqlDataReader.GetInt32(0),
        //                            Id = _SqlDataReader.GetInt32(0),
        //                            NumeroIdentificacion = _SqlDataReader.GetInt32(1),
        //                            Nombre = _SqlDataReader.GetString(2),
        //                            Apellido = _SqlDataReader.GetString(3),
        //                            Correo = _SqlDataReader.GetString(4),
        //                            FechaNacimiento = _SqlDataReader.GetDecimal(5),
        //                            FechaCreacion = _SqlDataReader.GetString(6),
        //                            FechaModificacion = _SqlDataReader.GetString(7),

        //                            TipoDocumentoId = _SqlDataReader.GetInt32(9),
        //                        });

        //                        //objStudent.UserNotifications = JsonConvert.DeserializeObject<UserNotifications>(_SqlDataReader.GetString(10).Replace("idStudent", "StudentID").Replace("[", "").Replace("]", ""));
        //                        //objStudent.ProfileInterests = JsonConvert.DeserializeObject<ProfileInterests>(_SqlDataReader.GetString(9).Replace("idStudent", "StudentID").Replace("[", "").Replace("]", ""));
        //                    }
        //                }
        //            }
        //            catch (Exception exception)
        //            {
        //                throw new Exception(string.Concat("GetPublicacionsDetalle() Exception: ", exception.Message));
        //            }
        //        }
        //        finally
        //        {
        //            await _SqlConnection.CloseAsync();
        //            await _SqlConnection.DisposeAsync();
        //        }
        //        return publicacionsDetalle;
        //    }

        //}
    }
}
