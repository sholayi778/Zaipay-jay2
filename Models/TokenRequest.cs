namespace Zaipay.Models
{
    public class TokenRequest
    {
        public string grant_type { get; set; } = "client_credentials";
        public string client_id { get; set; } = "5oqe8dmsqdke0c23pb3idu6866";
        public string client_secret { get; set; } = "7bjrhcukcom7hejlt5nbhav3oqvkmu2eob39sotcprcmkpbluih";
        public string scope { get; set; } = "im-au-05/e35399b0-7035-013a-64c3-0a58a9feac03:5c3627e8-3767-4bcc-b891-05b23ec59b16:3";
    }
}
