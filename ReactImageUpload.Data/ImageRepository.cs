namespace ReactImageUpload.Data
{
    public class ImageRepository
    {
        public string _connectionString { get; set; }

        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddPeople(List<Person> people)
        {
            using var context = new ImageDataContext(_connectionString);
            context.People.AddRange(people);
            context.SaveChanges();
        }
        public List<Person> GetPeople()
        {
            using var context = new ImageDataContext(_connectionString);
            return context.People.ToList();
        }
        public void DeleteAll()
        {
            using var context = new ImageDataContext(_connectionString);
            var peopleToRemove = context.People.ToList();
            context.People.RemoveRange(peopleToRemove);
            context.SaveChanges();
        }

    }
}