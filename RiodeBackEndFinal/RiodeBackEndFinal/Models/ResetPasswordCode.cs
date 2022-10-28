namespace RiodeBackEndFinal.Models
{
    public class ResetPasswordCode
    {
        public int id { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public TimeSpan ExpireTime { get; set; }
        public ResetPasswordCode(string userName)
        {
            Random random = new Random();
            Code = random.Next(99999,999999).ToString();
            ExpireTime = TimeSpan.FromMinutes(3);
            UserName = userName;
        }
    }
}
