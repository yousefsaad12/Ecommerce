namespace Api.Core.Models
{
    public interface ITokenInterface
    {
        public string GenerateJwtToke(AppUser appUser, string role);
    }
}