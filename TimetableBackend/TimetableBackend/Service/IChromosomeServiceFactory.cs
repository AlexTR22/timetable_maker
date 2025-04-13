namespace TimetableBackend.Service
{
    public interface IChromosomeServiceFactory
    {
        public ChromosomeService Create(string customParam);
    }
}
