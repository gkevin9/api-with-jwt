namespace ApiApplication.Model
{
    public class BaseUser
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public virtual bool IsUserDataIncomplete()
        {
            return string.IsNullOrEmpty(this.Username) ||
                string.IsNullOrEmpty(this.Password);
        }
    }
}
