using StockManagement.Core.DTOs.Usuario;
using System.Threading.Tasks;

namespace StockManagement.Core.Interfaces.Token
{
    public interface ITokenService
    {
        Task<RespostaDeLoginDto> CreateToken(string email);
    }
}
