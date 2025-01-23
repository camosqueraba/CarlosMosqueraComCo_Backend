using DAL.Model.Publicacion;
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

        public async Task<int> Create(Publicacion publicacion)
        {
            int idPublicacionCreated;
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

        public async Task<List<Publicacion>> GetAll()
        {
            List<Publicacion> publicacions = null;

            try
            {
                publicacions = await DBContext.Publicaciones.ToListAsync();
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

        public async Task<Publicacion> GetById(int id)
        {
            Publicacion publicacion = new Publicacion();
            try
            {
                publicacion = await DBContext.Publicaciones.AsNoTracking().FirstOrDefaultAsync(publicacion => publicacion.Id == id);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("GetById() Exception: ", ex.Message));
            }

            return publicacion;
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
