namespace WebApi.Models.Token
{
    public class RootObject
    {
        public bool Identified { get; set; }
        public Token Token { get; set; }
        public _Links _Links { get; set; }
    }
}