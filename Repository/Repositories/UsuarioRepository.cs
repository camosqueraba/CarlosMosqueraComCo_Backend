using DAL.DTOs.UsuarioDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.DataContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{   
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDBContext_SQLServer DBContext;
        private readonly UserManager<IdentityUser> userManager;
        public UsuarioRepository(ApplicationDBContext_SQLServer dbContext, UserManager<IdentityUser> userManager)
        {
            DBContext = dbContext;
            this.userManager = userManager;
        }
        /*
        public async Task<int> Create(IdentityUser IdentityUser)
        {
            int idIdentityUserCreated;
            Oper
            try
            {
                DBContext.Add(IdentityUser);
                await DBContext.SaveChangesAsync();
                idIdentityUserCreated = IdentityUser.Id;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("IdentityUserRepository.Create(IdentityUser IdentityUser) Exception: ", ex.Message));

            }

            return idIdentityUserCreated;
        }
        */
        public async Task<ResultadoOperacion<int>> Create(Usuario usuario)
        {
            ResultadoOperacion<int> resultadoOperacionCreate = new();
            try
            {
                /*
                var usuario = new IdentityUser
                {
                    UserName = credencialesUsuarioDTO.Email,
                    Email = credencialesUsuarioDTO.Email
                };
                */

                //DBContext.Add(IdentityUser);
                //await DBContext.SaveChangesAsync();
                //idIdentityUserCreated = IdentityUser.Id;
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    PhoneNumber = usuario.PhoneNumber

                };

                var resultado = await userManager.CreateAsync(identityUser);
                //DBContext.Add(identityUser);

                if (resultado.Succeeded)
                {
                    resultadoOperacionCreate.OperacionCompletada = true;
                }
                else
                {
                    resultadoOperacionCreate.OperacionCompletada = false;
                    resultadoOperacionCreate.Origen = "IdentityUserRepository.Create";
                    resultadoOperacionCreate.Error = resultado.Errors.ToString();
                }
                
            }
            catch (Exception ex)
            {
                resultadoOperacionCreate.OperacionCompletada = false;
                resultadoOperacionCreate.Origen = "IdentityUserRepository.Create";
                resultadoOperacionCreate.Error = ex.Message;
            }

            return resultadoOperacionCreate;
        }
                
        /*
        public async Task<ResultadoOperacion<int>> Delete(int id)
        {
            ResultadoOperacion<int> resultadoOperacionDelete = new();
            try
            {
                IdentityUser usuario = await DBContext.Users.FirstAsync(c => c.Id == id);

                DBContext.Remove(usuario);
                int resultado = await DBContext.SaveChangesAsync();

                if (resultado > 0)
                {
                    resultadoOperacionDelete.OperacionCompletada = true;
                    resultadoOperacionDelete.DatosResultado = 0;
                }
                else
                {
                    resultadoOperacionDelete.OperacionCompletada = false;
                    resultadoOperacionDelete.Origen = "IdentityUserRepository.Delete";
                    resultadoOperacionDelete.Error = "No se pudo eliminar de DB";
                }
            }
            catch (Exception ex)
            {
                resultadoOperacionDelete.OperacionCompletada = false;
                resultadoOperacionDelete.Origen = "IdentityUserRepository.Delete";
                resultadoOperacionDelete.Error = ex.Message;
            }
            return resultadoOperacionDelete;
        }
        */
        
        /*
        public async Task<int> Delete(int id)
        {
            int response;
            try
            {
                IdentityUser IdentityUser = await DBContext.Users.FirstAsync(c => c.Id == id);

                DBContext.Remove(IdentityUser);
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
        
        public async Task<List<Usuario>> GetAll()
        {
            List<Usuario> usuarios = null;
            List<IdentityUser> usuariosIdentity = null;

            try
            {
                usuariosIdentity = await DBContext.Users.ToListAsync();
                //var IdentityUsersTransform = IdentityUsers.Select(p => new { IdentityUser = p, ConteoComentarios = p.Comentarios.Count() });
            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("IdentityUserRepository.GetAll(IdentityUser IdentityUser) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("IdentityUserRepository.GetAll(IdentityUser IdentityUser) Exception: ", ex.Message));
            }


            return usuarios;
        }
        
        
        public async Task<ResultadoOperacion<UsuarioDetalleDTO>> GetById(string id)
        {
            IdentityUser usuario = null;
            ResultadoOperacion<UsuarioDetalleDTO> resultadoOperacion = new();
            try
            {
                usuario = await DBContext.Users.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.Id == id);

                resultadoOperacion.DatosResultado = IdentityUser;
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
        

        /*
        public async Task<int> Update(IdentityUser usuario)
        {
            int response;
            try
            {

                DBContext.Update(usuario);
                await DBContext.SaveChangesAsync();
                response = usuario.Id;

            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("IdentityUserRepository.Update(IdentityUser IdentityUser) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("IdentityUserRepository.Update(IdentityUser IdentityUser) Exception: ", ex.Message));
            }

            return response;
        }
        */

        public async Task<bool> ExisteUsuario(string email)
        {
            bool existeusuario = false;
            try
            {                
                var usuario = await userManager.FindByEmailAsync(email);

                if(usuario is not null)
                    existeusuario = true;
            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("IdentityUserRepository.ExisteIdentityUser(int id) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("IdentityUserRepository.ExisteIdentityUser(int id) Exception: ", ex.Message));
            }

            return existeusuario;
        }        

        /*
        public async Task<List<IdentityUser>> GetIdentityUsersDetalle()
        {
            var connectionString = _dbContext.Database.GetConnectionString();

            List<IdentityUser> IdentityUsersDetalle = new();


            using (SqlConnection _SqlConnection = new(connectionString))
            {
                try
                {
                    try
                    {
                        await _SqlConnection.OpenAsync();

                        SqlCommand _SqlCommand = new("dbo.SPGetDetalleIdentityUsers", _SqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        //_SqlCommand.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader _SqlDataReader = await _SqlCommand.ExecuteReaderAsync())
                        {
                            while (await _SqlDataReader.ReadAsync())
                            {
                                IdentityUsersDetalle.Add(new IdentityUser()
                                {
                                    //Id = _SqlDataReader.GetInt32(0),
                                    Id = _SqlDataReader.GetInt32(0),
                                    NumeroIdentificacion = _SqlDataReader.GetInt32(1),
                                    Nombre = _SqlDataReader.GetString(2),
                                    Apellido = _SqlDataReader.GetString(3),
                                    Correo = _SqlDataReader.GetString(4),
                                    FechaNacimiento = _SqlDataReader.GetDecimal(5),
                                    FechaCreacion = _SqlDataReader.GetString(6),
                                    FechaModificacion = _SqlDataReader.GetString(7),

                                    TipoDocumentoId = _SqlDataReader.GetInt32(9),
                                });

                                //objStudent.UserNotifications = JsonConvert.DeserializeObject<UserNotifications>(_SqlDataReader.GetString(10).Replace("idStudent", "StudentID").Replace("[", "").Replace("]", ""));
                                //objStudent.ProfileInterests = JsonConvert.DeserializeObject<ProfileInterests>(_SqlDataReader.GetString(9).Replace("idStudent", "StudentID").Replace("[", "").Replace("]", ""));
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(string.Concat("GetIdentityUsersDetalle() Exception: ", exception.Message));
                    }
                }
                finally
                {
                    await _SqlConnection.CloseAsync();
                    await _SqlConnection.DisposeAsync();
                }
                return IdentityUsersDetalle;
            }

        }
        */
    }
}