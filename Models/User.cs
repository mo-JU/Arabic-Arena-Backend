namespace Arabic_Arena.Models
{
    public class User
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string level {  get; set; }
        public string language { get; set; }
        public List<string> completedLessons {  get; set; }
        public List<string> completedQuizzes {  get; set; }
        
    }
}
