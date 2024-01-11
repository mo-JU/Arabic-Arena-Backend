namespace Arabic_Arena.Models
{
    public class User
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string level {  get; set; }
        public string language { get; set; }
        public List<CompletedTasks> completedLessons {  get; set; }
        public List<CompletedTasks> completedQuizzes {  get; set; }
        
    }

    public class CompletedTasks
    {
        public string id { get; set; }
        public string level { set; get; }
    }
}
