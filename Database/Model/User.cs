namespace Database.Model
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public byte[] Id { get; set; }

        public byte[] Iv { get; set; }

        public string Username { get; set; }

        public byte[] Password { get; set; }
    }
}
