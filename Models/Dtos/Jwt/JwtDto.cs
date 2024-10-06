namespace Models.Dtos.Jwt
{
    public class JwtDto
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public int ExpirationSeconds { get; set; }
    }
}
