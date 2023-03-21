using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaipay.Models
{
    public class TokenModel
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string BearerToken { get; set; }
        public DateTime MaintainedAt { get; set; }
    }
}
