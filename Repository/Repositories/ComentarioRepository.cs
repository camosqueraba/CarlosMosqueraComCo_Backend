using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using DAL.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.DataContext;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly ApplicationDBContext_SQLServer DBContext;
        public ComentarioRepository(ApplicationDBContext_SQLServer dbContext)
        {
            DBContext = dbContext;
        }

        public async Task<int> Create(Comentario comentario)
        {
            int idComentarioCreated;
            try
            {
                DBContext.Add(comentario);
                await DBContext.SaveChangesAsync();
                idComentarioCreated = comentario.Id;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("ComentarioRepository.Create(Comentario comentario) Exception: ", ex.Message));

            }

            return idComentarioCreated;
        }


        public async Task<int> Delete(int id)
        {
            int response;
            try
            {
                Comentario comentario = await DBContext.Comentarios.FirstAsync(c => c.Id == id);

                DBContext.Remove(comentario);
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

        public async Task<List<Comentario>> GetAll()
        {
            List<Comentario> comentarios = null;

            try
            {
                comentarios = await DBContext.Comentarios.ToListAsync();
            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("ComentarioRepository.GetAll(Comentario comentario) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("ComentarioRepository.GetAll(Comentario comentario) Exception: ", ex.Message));
            }


            return comentarios;
        }

        public async Task<Comentario> GetById(int id)
        {
            Comentario comentario = new Comentario();
            try
            {
                comentario = await DBContext.Comentarios.AsNoTracking().FirstOrDefaultAsync(comentario => comentario.Id == id);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("GetById() Exception: ", ex.Message));
            }

            return comentario;
        }


        public async Task<int> Update(Comentario comentario)
        {
            int response;
            try
            {

                DBContext.Update(comentario);
                await DBContext.SaveChangesAsync();
                response = comentario.Id;

            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("ComentarioRepository.Update(Comentario comentario) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("ComentarioRepository.Update(Comentario comentario) Exception: ", ex.Message));
            }

            return response;
        }


        public async Task<bool> ExisteComentario(int id)
        {
            bool existeComentario;
            try
            {
                bool result = await DBContext.Comentarios.AsNoTracking().AnyAsync(x => x.Id == id);
                existeComentario = result;
            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("ComentarioRepository.ExisteComentario(int id) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("ComentarioRepository.ExisteComentario(int id) Exception: ", ex.Message));
            }

            return existeComentario;
        }

        public async Task<List<Comentario>> GetComentariosPorIdPublicacion(int idPublicacion)
        {
            try
            {
                List<Comentario> comentarios = [];
                comentarios = await DBContext.Comentarios.Where(c => c.PublicacionId == idPublicacion).ToListAsync();
                return comentarios;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<List<Comentario>> GetComentariosDetalle()
        //{
        //    var connectionString = _dbContext.Database.GetConnectionString();

        //    List<Comentario> comentariosDetalle = new();


        //    using (SqlConnection _SqlConnection = new(connectionString))
        //    {
        //        try
        //        {
        //            try
        //            {
        //                await _SqlConnection.OpenAsync();

        //                SqlCommand _SqlCommand = new("dbo.SPGetDetalleComentarios", _SqlConnection)
        //                {
        //                    CommandType = CommandType.StoredProcedure
        //                };

        //                //_SqlCommand.Parameters.AddWithValue("@id", id);

        //                using (SqlDataReader _SqlDataReader = await _SqlCommand.ExecuteReaderAsync())
        //                {
        //                    while (await _SqlDataReader.ReadAsync())
        //                    {
        //                        comentariosDetalle.Add(new Comentario()
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
        //                throw new Exception(string.Concat("GetComentariosDetalle() Exception: ", exception.Message));
        //            }
        //        }
        //        finally
        //        {
        //            await _SqlConnection.CloseAsync();
        //            await _SqlConnection.DisposeAsync();
        //        }
        //        return comentariosDetalle;
        //    }

        //}
    }
}