using Models;

namespace Repositorios
{
    public interface IRepositorioUsuario
    {
        public List<Usuario>? GetAll();
    }
}