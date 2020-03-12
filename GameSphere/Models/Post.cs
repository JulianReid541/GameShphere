namespace GameSphere.Models
{
    public class Post
    {
        public int PostID { get; set; }       
        public string Message { get; set; }

        public AppUser UserName { get; set; }
        public int UserID { get; set; }
    }
}
