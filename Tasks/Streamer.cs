namespace Tasks
{
    public sealed class Streamer
    {
        private string _name;

        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
    }
}