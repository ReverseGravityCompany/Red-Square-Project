namespace Cafebazaar.Data
{
    public class GameData
    {
        public string Uid { get; private set; }
        public string Slug { get; private set; }

        public GameData(string uid, string slug)
        {
            this.Uid = uid;
            this.Slug = slug;
        }
    }
}