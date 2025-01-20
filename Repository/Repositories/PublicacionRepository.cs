using DAL.Model.Publicacion;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.DataContext;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class PublicacionRepository : IPublicacionRepository
    {
        private readonly ApplicationDBContext_SQLServer _dbContext;
        public PublicacionRepository(ApplicationDBContext_SQLServer dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(Publicacion publicacion)
        {
            int idPublicacionCreated;
            try
            {
                _dbContext.Add(publicacion);
                await _dbContext.SaveChangesAsync();
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

                Publicacion publicacion = await _dbContext.Publicaciones.FirstAsync(c => c.Id == id);


                _dbContext.Remove(publicacion);
                response = await _dbContext.SaveChangesAsync();


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
                publicacions = await _dbContext.Publicaciones.ToListAsync();
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
                publicacion = await _dbContext.Publicaciones.FirstOrDefaultAsync(publicacion => publicacion.Id == id);
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
                var existingPublicacion = _dbContext.Publicaciones.Local.SingleOrDefault(c => c.Id == publicacion.Id);
                if (existingPublicacion != null)
                {
                    _dbContext.Entry(existingPublicacion).State = EntityState.Detached;

                    publicacion.FechaCreacion = existingPublicacion.FechaCreacion;
                    publicacion.FechaModificacion = DateTime.Now;
                }

                _dbContext.Update(publicacion);
                await _dbContext.SaveChangesAsync();
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
