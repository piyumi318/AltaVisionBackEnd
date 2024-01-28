namespace SolarEcoBackEnd.DB
{
    public class IdFactory
    {
        public Id CreateId(string channel)
        {
            if (string.IsNullOrEmpty(channel))
                return null;

            switch (channel)
            {
                case "Customer":
                    return new CustomerId();
                case "Admin":
                    return new AdminId();
                
                default:
                    throw new ArgumentException("Unknown channel " + channel);
            };
        }
     }
}
