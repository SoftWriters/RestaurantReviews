namespace Models
{
    public class ChainModel : IChainModel
    {
        public ChainModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}