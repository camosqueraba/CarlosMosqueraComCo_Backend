using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.DataContext;
using Repository.IdentityEF;
using Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Repository.Repositories
{   
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDBContext_SQLServer DBContext;
        private readonly UserManager<CustomIdentityUser> UserManager;
        private readonly IConfiguration Configuration;
        
        public UsuarioRepository(ApplicationDBContext_SQLServer dbContext, UserManager<CustomIdentityUser> userManager, IConfiguration configuration)
        {
            DBContext = dbContext;
            this.UserManager = userManager;
            this.Configuration = configuration;
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

        public async Task<ResultadoOperacion<string>> Create(Usuario usuario)
        {
            ResultadoOperacion<string> resultadoOperacionCreate = new();
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
                CustomIdentityUser identityUser = new CustomIdentityUser
                {
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    PhoneNumber = usuario.PhoneNumber

                };

                //var resultado = await userManager.CreateAsync(identityUser);
                DBContext.Add(identityUser);
                int resultado = await DBContext.SaveChangesAsync();
                //DBContext.Add(identityUser);
                //var user = resultado.
                
                //if (resultado.Susseced)
                if (resultado > 0)
                {
                    resultadoOperacionCreate.OperacionCompletada = true;
                    resultadoOperacionCreate.DatosResultado = identityUser.Id;
                }
                else
                {
                    resultadoOperacionCreate.OperacionCompletada = false;
                    resultadoOperacionCreate.Origen = "IdentityUserRepository.Create";
                    //resultadoOperacionCreate.Error = resultado.Errors.ToString();
                }
                
            }
            catch (SqlException ex)
            {
                resultadoOperacionCreate.OperacionCompletada = false;
                resultadoOperacionCreate.Origen = "IdentityUserRepository.Create";
                resultadoOperacionCreate.Error = ex.Message;
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

        public async Task<ResultadoOperacion<int>> Delete(string id)
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


        public async Task<ResultadoOperacion<List<CustomIdentityUser>>> GetAll()
        {
            //List<Usuario> usuarios = null;
            List<CustomIdentityUser> usuariosIdentity = null;
            ResultadoOperacion<List<CustomIdentityUser>> resultadoOperacionGetAll = new();
            
            try
            {
                usuariosIdentity = await DBContext.Users.ToListAsync();
                //if(usuariosIdentity)
                resultadoOperacionGetAll.DatosResultado = usuariosIdentity;
                resultadoOperacionGetAll.OperacionCompletada = true;
                //var IdentityUsersTransform = IdentityUsers.Select(p => new { IdentityUser = p, ConteoComentarios = p.Comentarios.Count() });
            }            
            catch (Exception ex)
            {
                resultadoOperacionGetAll.Origen = "IdentityUserRepository.GetAll";
                resultadoOperacionGetAll.Error = ex.Message;
                //throw new Exception(string.Concat("IdentityUserRepository.GetAll(IdentityUser IdentityUser) Exception: ", ex.Message));
            }


            return resultadoOperacionGetAll;
        }
        
        
        public async Task<ResultadoOperacion<CustomIdentityUser>> GetById(string id)
        {
            CustomIdentityUser usuario = null;
            ResultadoOperacion<CustomIdentityUser> resultadoOperacion = new();
            try
            {
                usuario = await DBContext.Users.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.Id == id);

                resultadoOperacion.DatosResultado = usuario;
                /*
                new UsuarioDetalleDTO                
                {
                    UserName = usuario.UserName,
                    NormalizedUserName = usuario.NormalizedUserName,
                    Email = usuario.Email,
                    NormalizedEmail = usuario.NormalizedEmail,
                    PhoneNumber = usuario.PhoneNumber,
                    TwoFactorEnabled = usuario.TwoFactorEnabled,
                    LockoutEnd = usuario.LockoutEnd,
                    AccessFailedCount = usuario.AccessFailedCount,

                };
                */
                resultadoOperacion.OperacionCompletada = true;
            }
            catch (SqlException ex)
            {
                resultadoOperacion.Origen = "UsuarioRepository.GetAll";
                resultadoOperacion.Error = ex.Message;
            }

            return resultadoOperacion;
        }
                
        public async Task<ResultadoOperacion<int>> Update(CustomIdentityUser usuario)
        {
            ResultadoOperacion<int> resultUpdateRep = new();
            int response;
            try
            {

                DBContext.Update(usuario);
                response = await DBContext.SaveChangesAsync();
                
                if (response > 0)               
                    resultUpdateRep.OperacionCompletada = true;

            }            
            catch (Exception ex)
            {
                resultUpdateRep.OperacionCompletada = false;
                resultUpdateRep.Error = ex.Message + " | " + ex.InnerException;
                resultUpdateRep.Origen = "UsuarioRepositiry.Update";
            }

            return resultUpdateRep;
        }

       
        

        public async Task<ResultadoOperacion<RespuestaAutenticacionDTO>> RegistrarUsuario(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            ResultadoOperacion<RespuestaAutenticacionDTO> resultadoOperacion = new();

            try
            {
                //RespuestaAutenticacionDTO respuestaAutenticacion;
                
                var usuario = new CustomIdentityUser
                {
                    UserName = credencialesUsuarioDTO.Email,
                    Email = credencialesUsuarioDTO.Email
                };

                //var resultado = await UsuarioService.RegistrarUsuario(credencialesUsuarioDTO);
                IdentityResult resultado = await UserManager.CreateAsync(usuario, credencialesUsuarioDTO.Password);

                if (resultado.Succeeded)
                {
                    RespuestaAutenticacionDTO respuestaAutenticacion = await ConstruirToken(credencialesUsuarioDTO);
                    resultadoOperacion.DatosResultado = respuestaAutenticacion;
                    resultadoOperacion.OperacionCompletada = true;
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        resultadoOperacion.Error +=  error.Description + " ";
                    }

                    resultadoOperacion.OperacionCompletada = false;
                    resultadoOperacion.Origen = "UsuarioRepository.Registrar";
                }

                return resultadoOperacion;
            }
            catch (Exception ex)
            {
                resultadoOperacion.OperacionCompletada = false;
                resultadoOperacion.Origen = "UsuarioRepository.Registrar";
                resultadoOperacion.Error = ex.Message;

                return resultadoOperacion;
            }
           
        }

        public async Task<bool> ExisteUsuario(string email)
        {
            bool existeusuario = false;
            try
            {                
                var usuario = await UserManager.FindByEmailAsync(email);

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

        public async Task<bool> ExisteUsuarioById(string id)
        {
            bool existeusuario = false;
            try
            {
                //var usuario = await UserManager.FindByIdAsync(id);
                var usuario = await DBContext.Users.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.Id == id);

                if (usuario is not null)
                    existeusuario = true;
            }
            catch (SqlException ex)
            {

                throw new Exception(string.Concat("UsuarioRepository.ExisteUsuarioById(string id) Exception: ", ex.Message));
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat("UsuarioRepository.ExisteUsuarioById(string id) Exception: ", ex.Message));
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

        private async Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {

            var claims = new List<Claim>
            {
                new Claim("email", credencialesUsuarioDTO.Email)
            };

            var usuario = await UserManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
            var claimsDB = await UserManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimsDB);
            var llave = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["llavejwt"]!));
            var credenciales = new Microsoft.IdentityModel.Tokens.SigningCredentials(llave, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var tokenDeSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: credenciales);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);

            return new RespuestaAutenticacionDTO
            {
                Token = token,
                Expiracion = expiracion
            };

        }

    }
}