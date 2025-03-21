namespace BookMyMovie.Application.Common.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(Guid userId, string role, string userName,string FirstName );
    
}