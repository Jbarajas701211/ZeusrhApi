﻿using Interfaces.IBussiness;
using Interfaces.IRepository;
using Interfaces.Utilitys;
using Models;
using Models.DTOs;
using Models.Entities;

namespace Implementation.Bussiness
{
    public class ManagementUsuario : IManagementUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUtility _utility;

        public ManagementUsuario(IUsuarioRepository usuarioRepository, IUtility utility)
        {
            _usuarioRepository = usuarioRepository;
            _utility = utility;
        }

        public async Task<ApiResponse<RespuestaAutenticacionDTO>> RegistroUsuario(Usuario usuario)
        {
            try
            {
                var existeUsuario = await ValidaUsuario(usuario.Correo ?? string.Empty);

                if (existeUsuario is not null)
                {
                    return new ApiResponse<RespuestaAutenticacionDTO>() { Success = false, Errors = new List<string>() { "Usuario ya existente" } };
                };

                var seCreoUsuario = await _usuarioRepository.CrearUsuarioAsync(usuario);

                if (!seCreoUsuario)
                {
                    return new ApiResponse<RespuestaAutenticacionDTO>() { Success = false, Errors = new List<string>() { "No se pudo registrar el usuario" } };
                }
                else
                {
                    var token = _utility.GenerarJWT(usuario);

                    return new ApiResponse<RespuestaAutenticacionDTO> { Success = true, Data = token };
                }

                
            }
            catch (Exception ex)
            {
                return new ApiResponse<RespuestaAutenticacionDTO> { Success = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<ApiResponse<RespuestaAutenticacionDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                var existeUsuario = await ValidaUsuario(loginDTO.Correo);

                if(existeUsuario is null)
                {
                    return new ApiResponse<RespuestaAutenticacionDTO> { Errors = new List<string> { "Usuario no registrado " } };
                }

                if (existeUsuario.EsBloqueado)
                {
                    return new ApiResponse<RespuestaAutenticacionDTO> { Errors = new List<string> { "Usuario Bloqueado " } };
                }

                var passwordValido = PasswordCorrecto(loginDTO.Clave, existeUsuario.Clave!);

                if (!passwordValido)
                {
                    var intentosUsuario = await _usuarioRepository.ObtenerIntentosUsuarioAsync(existeUsuario.IdUsuario);
                    if (intentosUsuario is null)
                    {
                        await _usuarioRepository.CrearIntentosAsync(new UsuarioIntento { Intentos = 1, UsuarioId = existeUsuario.IdUsuario, Bloqueado = false });
                        return new ApiResponse<RespuestaAutenticacionDTO> { Errors = new List<string> { $"Solo tienes 2 intentos de 3 para ingresar el password correcto" } };
                    }

                    intentosUsuario!.Intentos += 1;

                    if (intentosUsuario.Intentos > 3)
                    {
                        intentosUsuario.Bloqueado = true;
                        intentosUsuario.FechaBloqueo = DateTime.Now;

                        var seBloqueo = await _usuarioRepository.ActualizarIntentosAsync(intentosUsuario);

                        if (seBloqueo)
                        {
                            existeUsuario.EsBloqueado = true;
                            seBloqueo = await _usuarioRepository.ActualizarUsuarioBloquearAsync(existeUsuario);
                        }

                        return new ApiResponse<RespuestaAutenticacionDTO> { Errors = new List<string>() { "Tu usuario fue bloqueado " } };
                    }

                    await _usuarioRepository.ActualizarIntentosAsync(intentosUsuario);

                    return new ApiResponse<RespuestaAutenticacionDTO> { Errors = new List<string> { $"Solo tienes {3 - intentosUsuario.Intentos} intentos de 3 para ingresar el password correcto" } };
                }

                var token = _utility.GenerarJWT(existeUsuario);
                return new ApiResponse<RespuestaAutenticacionDTO> { Data = token, Success = true };
            }
            catch (Exception ex)
            {

                return new ApiResponse<RespuestaAutenticacionDTO> {  Errors = new List<string> { ex.Message } };
            }
        }

        private bool PasswordCorrecto(string password, string passwordBd)
        {
            if(password == passwordBd)
            {
                return true;
            }
            return false;
        } 

        private async Task<Usuario?> ValidaUsuario(string correo)
        {
            return await _usuarioRepository.ObtenerUsuarioPorCorreoAsync(correo ?? string.Empty);
        }
    }
}
