using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.IRepository
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerUsuarioPorCorreoAsync(string correo);
        Task<bool> ActualizarIntentosAsync(UsuarioIntento usuarioIntento);
        Task<bool> CrearIntentosAsync(UsuarioIntento usuarioIntento);
        Task<bool> CrearUsuarioAsync(Usuario usuario);
        Task<UsuarioIntento?> ObtenerIntentosUsuarioAsync(int usuarioId);
    }
}
