﻿using System.Collections.Generic;

namespace BulkeyBook.Models.Razorepay
{
    public class RegistrationModel
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }

        public Dictionary<string ,string>? notes { get; set; }
    }
}
